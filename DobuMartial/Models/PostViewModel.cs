namespace DobuMartial_project.Models
{
    public class PostViewModel
    {
        public ForumPost? Post { get; set; }
        public string Comment { get; set; } = "";
        public string UserId { get; set; } = "";
    }
}
