using DobuMartial_project.Data;
using DobuMartial_project.Models;
using DobuMartial_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
}