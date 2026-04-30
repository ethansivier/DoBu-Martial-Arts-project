using System.Reflection.Metadata;

namespace DobuMartial_project.Models
{
    public class ForumModel
    {
        public List<ForumPost?> Posts { get; set; } = new List<ForumPost?>();
        public string PostBody { get; set; } = "";
        public string PostTitle { get; set; } = "";
    }
}
