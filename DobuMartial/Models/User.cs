using Microsoft.AspNetCore.Identity;

namespace DobuMartial_project.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public List<Session> Sessions { get; } = [];
        public List<Class> ChosenMartialArts { get; set; } = new List<Class>();
        public Membership?  Membership { get; set; } // nullable so that users dotn HAVE to have a membership to exist (issue cause dby seeding admin without membership causing crashes)
    }
}
