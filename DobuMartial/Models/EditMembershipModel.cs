namespace DobuMartial_project.Models
{
    public class EditMembershipModel
    {
        public List<Membership?> Memberships { get; set; }
        public Membership? ChosenMembership { get; set; }
        public int? ChosenMembershipID { get; set; }
        public string newName { get; set; } = "";
        public int newPrice { get; set; }
        public int newMartialArts { get; set; }
        public int newSessions { get; set; }
        public bool newIsKids { get; set; }
        public bool update { get; set; } = false;
    }
}
