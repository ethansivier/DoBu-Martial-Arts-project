using System.ComponentModel.DataAnnotations;

namespace DobuMartial_project.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="Please enter fullname")]
        public string Fullname { get; set; }
        [Required(ErrorMessage ="Please enter email"), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter password.")]
        public string Password { get; set; }
    }
}
