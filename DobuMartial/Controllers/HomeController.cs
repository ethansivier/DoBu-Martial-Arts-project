using DobuMartial_project.Data;
using DobuMartial_project.Models;
using DobuMartial_project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DobuMartial_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly DBInfoGrabber _dbGrabber;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> userManager, DBInfoGrabber dbInfoGrabber)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _dbGrabber = dbInfoGrabber;
        }

        public async Task<User?> GetUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

       
        public IActionResult ErrorRedirect(string error, string action, string controller, string fragment = "")
        {
            TempData["Error"] = error;
            return RedirectToAction(action, controller, fragment);
        }
        public async Task<IActionResult> Index()
        {
            User? user = await GetUser();

            var IndexModel = new IndexModel();
            if (user != null)
            {
                var HasMembership = _context.Users.Include(u => u.Membership).FirstOrDefault(u => u.Id == user.Id);
                User? dbUser = _context.Users.Find(user.Id);
                IndexModel.CurrentMembership = dbUser?.Membership != null ? dbUser.Membership : new Membership();
            }


            IndexModel.Instructors = await _context.Instructors.ToListAsync();
            IndexModel.Memberships = await _context.Memberships.ToListAsync();
            return View(IndexModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public async Task<IActionResult> Timetable(TimetableModel model)
        {
            var days = await _context.WeekDays.Include(d => d.Sessions).ThenInclude(s => s.Class).ToListAsync();

            model.Days = days;
            
            model.Day = (model.SelectedDayId == 0) ? days[0] : days[model.SelectedDayId - 1]; //tenary since default is 0, but when posting from page monday = 1 instead of 0
            
            
            return View(model);
        }

        // gets all sessions and returns as int, excludes private training as thye do not count to martial art ones
        public int GetSessionsCount(List<Session> Sessions, List<string> InvalidClasses)
        {
            int ses = 0;
            foreach (var session in Sessions)
            {
                if (!(InvalidClasses.FirstOrDefault(session.Class.Name) == null))
                {
                    ses++;
                }
            }
            return ses;
        }

       

        [HttpPost]
        public async Task<IActionResult> Book(int sessionid, int dayid)
        {
            User? idUser = await GetUser(); //user from identity
            List<string> InvalidClasses = ["Private Tuition"]; //invaliud classes for checking booking

            RedirectToActionResult? EndRedirect = RedirectToAction("Timetable", "Home", new { SelectedDayId = dayid });
            if (idUser == null) { return ErrorRedirect("Please sign in before attemping to select classes.", "Register", "Account");  }
            Session? dbSession = await _dbGrabber.GetDBSession(sessionid);
            User? dbUser =  await _dbGrabber.GetDBUser(idUser);
            

            if (dbSession == null) { return ErrorRedirect("Could not find session", "Register", "Account"); } //not in db, failsafe to login 
            if (dbUser == null) { return ErrorRedirect("Could not find user", "Register", "Account"); } //not in db, failsafe to login 
            Membership? membership = dbUser.Membership;

            if (membership == null) { return ErrorRedirect("Please subscribe to a membership before booking.", "Index", "Home", "prices"); }
            //checking if they can book for this martial art type

            if (dbUser.Membership.IsKids & dbSession.Class.IsKids)
            {
                TempData["Success"] = TempData["Success"] = $"Successfully added {dbSession?.Class.Name} on {dbSession?.Day.Name.ToString()}" +
                    $" at {dbSession?.TimeStart.ToString()} to your booking ";
                return EndRedirect;
            }
            else if (dbSession.Class.IsKids & dbUser.Membership.IsKids == false)
            {
                return ErrorRedirect($"You cannot join this class as it is a kids only class. Your  {dbUser.Membership.Name} membership does not allow for this.",
                    EndRedirect.ActionName, EndRedirect.ControllerName, EndRedirect.Fragment);
            }
            else if (dbSession.Class.IsKids == false & dbUser.Membership.IsKids == true)
            {
                return ErrorRedirect($"You cannot join this class as it is a adults only class. The  {dbUser.Membership.Name} membership does not allow for this.",
                    EndRedirect.ActionName, EndRedirect.ControllerName, EndRedirect.Fragment);
            }

            if (dbUser.ChosenMartialArts.Count < membership?.MartialArts)
            {
                if (!dbUser.ChosenMartialArts.Contains(dbSession.Class))
                {
                    dbUser.ChosenMartialArts.Add(dbSession.Class);
                }
            }
            //checking if they can book anymore sessions

            if (GetSessionsCount(dbUser.Sessions, InvalidClasses) >= membership?.Sessions)
            {
                return ErrorRedirect($"You are at the maximum booked sessions for your membership, " +
                    $"the {membership.Name} plan only allows {membership.MartialArts.ToString()}. " +
                    $"Please remove other sessions, or upgrade membership to add more.", EndRedirect.ActionName, EndRedirect.ControllerName, EndRedirect.Fragment);
            }
                
            if(InvalidClasses.FirstOrDefault(dbSession?.Class.Name) == null)
            {
                return ErrorRedirect($"You cannot currently choose any more martial art types, " +
                    $"as your membership {dbUser?.Membership?.Name} only allows {dbUser?.Membership?.MartialArts.ToString()}" +
                    $"you can currently only book classes for {string.Join(",", dbUser.ChosenMartialArts.Select(c => c.Name))}", 
                    EndRedirect.ActionName, EndRedirect.ControllerName, EndRedirect.Fragment);
            }
            if(dbUser.Sessions.Any(s => s.SessionId == sessionid))
            {
                return ErrorRedirect($"You already are signed up for this session.", EndRedirect.ActionName, EndRedirect.ControllerName, EndRedirect.Fragment);
            }

            TempData["Success"] = $"Successfully added {dbSession?.Class.Name} on {dbSession?.Day.Name.ToString()} at {dbSession?.TimeStart.ToString()} to your booking ";
            dbUser.Sessions.Add(dbSession);

            await _context.SaveChangesAsync();
            
            return EndRedirect;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string ChosenMembership)
        {
            User? idUser = await GetUser();

            if (idUser != null)
            {
                Membership? Membership = await _context.Memberships.FirstOrDefaultAsync(p => p.Name == ChosenMembership);
                if (Membership != null)
                {
                    User? user = await _dbGrabber.GetDBUser(idUser);
                    if (user == null) { return ErrorRedirect("Unexpected error whilst attemping to subscribe", "Index", "Home", "prices"); }
                    user.Membership = Membership;
                    user.Sessions.Clear(); // rem,ove their sessions incase they go from elite -> basic 

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Timetable");
                }
                else
                {
                    return ErrorRedirect("Could not find that membership in the database.", RouteData.Values["action"].ToString(), RouteData.Values["controller"].ToString());
                }
            }
            else
            {
                return ErrorRedirect("You must sign in before you can select a membership.", "Register", "Account");
            } 
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
