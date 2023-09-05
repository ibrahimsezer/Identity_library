using System.ComponentModel.DataAnnotations;

namespace Identity_library.Domain.Models
{
    public class PasswordResetModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        public string NewConfirmPassword { get; set; }

    }
}
