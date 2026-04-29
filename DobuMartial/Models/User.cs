using DobuMartial_project.Services;
using Microsoft.AspNetCore.Identity;

namespace DobuMartial_project.Models
{
    public class User:IdentityUser
    {
        /*private readonly DBInfoGrabber _dbGrabber;

        public User(DBInfoGrabber dbGrabber)
        {
            _dbGrabber = dbGrabber;
        }*/
        public string? FullName { get; set; }
        public List<Session> Sessions { get; } = [];
        public List<Class> ChosenMartialArts { get; set; } = new List<Class>();
        public Membership?  Membership { get; set; } 

        public int GetSessionCount()
        {
            int count = 0;
            foreach (Session session in Sessions)
            {
                count ++;
            }
            return count;
        }

    }
}
