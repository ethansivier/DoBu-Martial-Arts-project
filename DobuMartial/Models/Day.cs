namespace DobuMartial_project.Models
{
    public class Day
    {
        public int DayID { get; set; }
        public string Name { get; set; }

        public List<Session> Sessions { get; } = new List<Session>();
    }
}
