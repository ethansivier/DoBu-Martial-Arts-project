namespace DobuMartial_project.Models
{
    public class TimetableModel
    {
        public List<Day> Days { get; set; }
        public Day Day { get; set; }
        public int SelectedDayId { get; set; }
    }
}
