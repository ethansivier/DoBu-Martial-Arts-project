using DobuMartial_project.Data;
using DobuMartial_project.Models;
using DobuMartial_project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace DobuMartial_project.Controllers
{
    

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DBInfoGrabber _dbGrabber;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DBInfoGrabber dbGrabber, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbGrabber = dbGrabber;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ErrorRedirect(string error, string action, string controller, string fragment = "")
        {
            TempData["Error"] = error;
            return RedirectToAction(action, controller, fragment);
        }

        public async Task<dynamic> GetUser()
        {
            User? idUser = await _userManager.GetUserAsync(HttpContext.User);
            if (idUser == null) { return ErrorRedirect("An error happened, try again", RouteData.Values["action"].ToString(), RouteData.Values["controller"].ToString()); }
            User? dbUser = await _dbGrabber.GetDBUser(idUser);
            if (dbUser == null) { return ErrorRedirect("An error happened, try again", RouteData.Values["action"].ToString(), RouteData.Values["controller"].ToString()); }

            return dbUser;
        }

      

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser regUser)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = regUser.Email,
                    Email = regUser.Email,
                    FullName = regUser.Fullname
                };
                var result = await _userManager.CreateAsync(user, regUser.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        TempData["Error"] += error.Description ;
                    }
                }
            }
            return View(regUser);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return ErrorRedirect("Invalid Login", "Login", "Account");
                }
            }
            return ErrorRedirect("Invalid Login", "Login", "Account");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");  
        }

        public async Task<IActionResult> ChosenSessions(User user)
        { 
            var result = await GetUser();
            if (result is IActionResult)
            {
                return (IActionResult)result;
            }
            user = (User)result;
            return View(user);
        }

        public async Task<IActionResult> RemoveSession(int sessionid)
        {
            var result = await GetUser();
            if (result is IActionResult)
            {
                return (IActionResult)result;
            }
            User user = (User)result;
            Session? dbSession = await _dbGrabber.GetDBSession(sessionid);
            if (dbSession == null) { return ErrorRedirect("Could not find requested session. Please try again", "ChosenSessions", "Account"); }
            user.Sessions.RemoveAll(s => s.SessionId == dbSession.SessionId);
            await _context.SaveChangesAsync();
            return RedirectToAction("ChosenSessions", "Account");
        }
    }
}
