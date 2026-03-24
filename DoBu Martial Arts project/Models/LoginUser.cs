using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DoBu_Martial_Arts_project.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage ="Please enter enmail"), EmailAddress]
        public string Email{ get; set; }
        [Required(ErrorMessage ="Pleaes enter password")]
        public string Password { get; set; }
    }
}
