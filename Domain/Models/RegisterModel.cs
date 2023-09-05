using System.ComponentModel.DataAnnotations;

namespace Identity_library.Domain.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage ="Email required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Username required.")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Phone Number required")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
