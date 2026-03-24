using Microsoft.AspNetCore.Identity;

namespace DoBu_Martial_Arts_project.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
