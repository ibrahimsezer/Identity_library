using System.ComponentModel.DataAnnotations;

namespace Identity_library.Domain.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
