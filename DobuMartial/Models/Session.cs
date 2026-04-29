using System.ComponentModel.DataAnnotations.Schema;

namespace DobuMartial_project.Models
{
    public class Session
    {
        public int? SessionId { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public bool Selected { get; set; } // used for admin page

        [ForeignKey("DayID")]
        public int DayID { get; set; }
        public Day Day { get; set; } = null!;
        [ForeignKey("ClassID")]
        public int? ClassId { get; set; }
        public Class Class {get;set;}
        public List<User> Users { get; set; } = new List<User>();

        public string TimeFormatted()
        {
            return $"{TimeStart.ToString()} {(TimeStart >= new TimeOnly(11, 00) ? "PM" : "AM")} - {TimeEnd.ToString()}{(TimeStart >= new TimeOnly(11, 00) ? "PM" : "AM")}";
        }
    }
}
