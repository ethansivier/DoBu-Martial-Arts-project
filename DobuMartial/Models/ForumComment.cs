using System.ComponentModel.DataAnnotations.Schema;

namespace DobuMartial_project.Models
{
    public class ForumComment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Comment { get; set; } = "";
        public DateTime Time { get; set; }

        [ForeignKey("PostId")]
        public ForumPost Post { get; set; }
        public int PostId { get; set; }
    }
}
