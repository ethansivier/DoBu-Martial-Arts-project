using Microsoft.AspNetCore.Identity;

namespace DobuMartial_project.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
