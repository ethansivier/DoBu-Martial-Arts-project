using Microsoft.EntityFrameworkCore;

namespace DobuMartial_project.Models
{
    [PrimaryKey(nameof(PostId))]
    public class ForumPost
    {
        public int PostId { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public string PostTitle { get; set; } = "title";
        public string PostBody { get; set; } = "text";
        public DateTime PostDate { get; set; }
        public List<ForumComment> Comments { get; set; } = new List<ForumComment>();
    }
}
