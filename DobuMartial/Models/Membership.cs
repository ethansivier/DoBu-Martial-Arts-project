namespace DobuMartial_project.Models
{
    public class Membership
    {
        public int MembershipId { get; set; }
        public string Name { get; set; }
        // how many martial art types  they can book
        public int MartialArts { get; set; }
        //how many sessions tehy can book 
        public int Sessions { get; set; }
        public double Price { get; set; }
        public bool IsKids { get;set; }
    }
}
