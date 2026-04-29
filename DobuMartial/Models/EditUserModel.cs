using System.Reflection.Metadata.Ecma335;

namespace DobuMartial_project.Models
{
    public class EditUserModel
    {
        public User? User { get; set; } = null;
        public string Email { get; set; } = "";
        public List<Membership> Memberships { get; set; } = [];
    }
}
