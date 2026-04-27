using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DobuMartial_project.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage ="Please enter email"), EmailAddress]
        public string Email{ get; set; }
        [Required(ErrorMessage ="Please enter password")]
        public string Password { get; set; }

        public string LoginError { get; set; } = "";
    }
}
