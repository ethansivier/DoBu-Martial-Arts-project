using DobuMartial_project.Data;
using DobuMartial_project.Models;
using Microsoft.EntityFrameworkCore;

namespace DobuMartial_project.Services
{
    public class DBInfoGrabber
    {
        private readonly ApplicationDbContext _context;

        public DBInfoGrabber(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetDBUser(User user)
        {
            return await _context.Users
                .Include(u => u.Membership)
                .Include(u => u.Sessions).ThenInclude(s => s.Day).Include(u => u.Sessions).ThenInclude(s => s.Class)
                .Include(u => u.ChosenMartialArts)
                .FirstOrDefaultAsync(u => u.Id == user.Id); ;
        }

        public async Task<Session?> GetDBSession(int sessionID)
        {
            return await _context.Sessions
                .Include(s => s.Class)
                .Include(s => s.Day)
                .FirstOrDefaultAsync(s => s.SessionId == sessionID);
        }


        public async Task<Membership?> GetDBMembership(int membershipID)
        {
            return await _context.Memberships
                .FirstOrDefaultAsync(m => m.MembershipId == membershipID);
        }
    }
}
