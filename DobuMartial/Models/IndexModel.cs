namespace DobuMartial_project.Models
{
    public class IndexModel
    {
        public List<Instructor> Instructors { get; set;}
        public List<Membership> Memberships { get; set; }
        public Membership ChosenMembership { get; set; }
        public Membership CurrentMembership { get; set; } = new Membership();
    }
}
