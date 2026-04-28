using Microsoft.EntityFrameworkCore;

namespace DobuMartial_project.Models
{

    public class UserSession
    {
        int? UserId { get; set; }
        int? SessionId { get; set; }
    }
}
