namespace DobuMartial_project.Models
{
    public class DashboardModel
    {
        public int UserCount { get; set; }
        public int SessionCount { get; set; }
        public List<(string, int)> MembershipCount { get; set; } = new List<(string, int)>();
    }
}
