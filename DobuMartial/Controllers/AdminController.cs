using DobuMartial_project.Data;
using DobuMartial_project.Models;
using DobuMartial_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly DBInfoGrabber _dbGrabber;
    private readonly ApplicationDbContext _context;

    public AdminController(UserManager<User> userManager, DBInfoGrabber dbGrabber, ApplicationDbContext context)
    {
        _userManager = userManager;
        _dbGrabber = dbGrabber;
        _context = context;
    }

    public IActionResult ErrorRedirect(string error, string action, string controller, string fragment = "")
    {
        TempData["Error"] = error;
        return RedirectToAction(action, controller, fragment);
    }

    public async Task<IActionResult> Index(DashboardModel dashboardModel)
    {
        List<(string, int)> memCount = [];
        List<User> Users = [];
        int totalSessions = 0;

        foreach (User user in _context.Users)
        {
            User? dbUser = await _dbGrabber.GetDBUser(user);
            if (dbUser == null) { continue; }
            foreach (Session sesion in dbUser.Sessions)
            {
                totalSessions++;
            }
            Users.Add(dbUser);
        }

        foreach (Membership membership in _context.Memberships)
        {
            Membership? dbMembership = await _dbGrabber.GetDBMembership(membership.MembershipId);
            if (dbMembership == null) { continue; }
            int count = 0;
            foreach (User dbUser in Users)
            {
                if (dbUser == null || dbUser.Membership == null || dbMembership == null) { continue; }
                if (dbUser.Membership.MembershipId == dbMembership.MembershipId)
                {
                    count++;
                }
            }
            memCount.Add((dbMembership.Name, count));
        }

        dashboardModel.MembershipCount = memCount;
        dashboardModel.UserCount = Users.Count();
        dashboardModel.SessionCount = totalSessions;
        return View(dashboardModel);
    }

    public async Task<IActionResult> UserList(List<User> UserList)
    {
        UserList = [];
        foreach (User idUser in _context.Users)
        {
            User? dbUser = await _dbGrabber.GetDBUser(idUser);
            if (dbUser == null) { continue; }
            UserList.Add(dbUser);
        }
        return View(UserList);
    }

    [HttpGet]
    public async Task<IActionResult> UserEdit(EditUserModel editUserModel, string userid)
    {
        User? idUser = await _userManager.FindByIdAsync(userid);
        if (idUser == null) { return ErrorRedirect($"Could not find user for id {userid}", "Admin", "Index"); }
        User? dbUser = await _dbGrabber.GetDBUser(idUser);
        if (dbUser == null) { return ErrorRedirect($"Could not find user for id {userid}", "Admin", "Index"); }
        
        editUserModel.User = dbUser;
        editUserModel.newUsername = dbUser.UserName;
        editUserModel.Memberships = _context.Memberships.ToList();
        editUserModel.newSessions = dbUser.Sessions.ToList();
        editUserModel.newMembershipId = dbUser.Membership?.MembershipId;
        
      

        return View(editUserModel);
    }
    [HttpPost]
    public async Task<IActionResult> UserEdit(EditUserModel editUserModel)
    {
        var selectedSessions = editUserModel.newSessions.Where(s => s.Selected == false).ToList();
        var sessions = _context.Sessions.Where(s => selectedSessions.Contains(s));
        User ? idUser = await _userManager.FindByIdAsync(editUserModel.UserId);
        if (idUser == null) { TempData["Error"] = "FAILED TO FIND USER"; return View(); }

        User? dbUser = await _dbGrabber.GetDBUser(idUser);
        if (dbUser == null) { TempData["Error"] = "FAILED TO FIND USER"; return View(); }

        var token = await _userManager.GeneratePasswordResetTokenAsync(idUser);
        await _userManager.ResetPasswordAsync(idUser, token, editUserModel.newPassword);

        if (editUserModel.newMembershipId != null)
        {
            Membership membership = (Membership)_context.Memberships.First(m => m.MembershipId == editUserModel.newMembershipId);
            dbUser.Membership = membership;
        }

        dbUser.Sessions.Clear();
        dbUser.Sessions.AddRange(sessions);
        dbUser.UserName = editUserModel.newUsername;
        

        await _context.SaveChangesAsync();
        return RedirectToAction("UserList");
    }

    public async Task<IActionResult> UserDelete(string userid)
    {
        User? idUser = await _userManager.FindByIdAsync(userid);

        User? dbUser = await _dbGrabber.GetDBUser(idUser);
        if (dbUser == null) { return ErrorRedirect("COULD NOT FIND USER.", "UserList", "Admin"); }
        _context.Remove(dbUser);
        await _context.SaveChangesAsync();
        return RedirectToAction("UserList");
    }


    public async Task<IActionResult> MembershipEdit(EditMembershipModel model)
    {
        
        if (model.Memberships == null)
        {
            model.Memberships = _dbGrabber.GetAllDBMemberships();
        }
        if (model.ChosenMembershipID != null)
        {
            Membership m = await _dbGrabber.GetDBMembership((int)model.ChosenMembershipID);
            if (m != null)
            {
                model.ChosenMembership = m;
                if (model.update)
                {
                    m.Price = model.newPrice;
                    m.Name = model.newName;
                    m.Sessions = model.newSessions;
                    m.IsKids = model.newIsKids;
                    m.MartialArts = model.newMartialArts;
                    await _context.SaveChangesAsync(); 
                }
                else
                {
                    model.newIsKids = m.IsKids;
                }
            }
           
        }
        return View(model);
    }

    public async Task<IActionResult> MembershipCreate(EditMembershipModel model)
    {
        if (model.update == true)
        {
            Membership membership = new Membership();
            membership.Name = model.newName;
            membership.Price = model.newPrice;
            membership.Sessions = model.newSessions;
            membership.MartialArts = model.newMartialArts;
            membership.IsKids = model.newIsKids;

            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();
        }
        return View(model);
    }

    public async Task<IActionResult> MembershipRemove(EditMembershipModel model)
    {
        if (model.ChosenMembershipID != null)
        {
            Membership m = await _dbGrabber.GetDBMembership((int)model.ChosenMembershipID);
            if (m != null)
            {
                List<User?> allUsers = _dbGrabber.GetAllDbUsers();
                var usersWithMem = allUsers.Where(u => u.Membership?.MembershipId == m.MembershipId);
                foreach (User? u in usersWithMem)
                {
                    u.Membership = null;
                }
                _context.Memberships.Remove(m);
                
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("MembershipEdit");
    }
}