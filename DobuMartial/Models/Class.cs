using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace DobuMartial_project.Models
{
    public class Class
    {
        public int? ClassID { get; set; }
        public string Name { get; set; }

        public List<Session> Sessions { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsKids { get; set; }
    }
    
}
