using System.Reflection.Metadata.Ecma335;

namespace DobuMartial_project.Models
{
    public class EditUserModel
    {
        public User? User { get; set; } = null;
        public string? UserId { get; set; }
        public List<Membership> Memberships { get; set; } = [];
        public string? newUsername { get; set; } = "";
        public string newPassword { get; set; } = "";
        public int? newMembershipId { get; set; }

        public List<Session> newSessions { get; set; } = [];
    }
}
