using System.ComponentModel.DataAnnotations.Schema;

namespace DobuMartial_project.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public string Name { get; set; }
        public string Day { get; set; }
        public TimeOnly TimeStart { get; set; }
        public TimeOnly TimeEnd { get; set; }

        [ForeignKey("Instructor")]
        public string InstructorID { get; set; }
    }
}
