using System.ComponentModel.DataAnnotations;

namespace Identity_library.Domain.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "E-posta alanı gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
