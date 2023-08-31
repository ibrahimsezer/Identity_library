using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
      
            private readonly UserManager<IdentityUser> _userManager;
            private readonly SignInManager<IdentityUser> _signInManager;

            public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Post([FromBody] LoginModel model)
            {
                // Kullanıcıyı doğrulayın.
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return BadRequest("Kullanıcı adı bulunamadı.");
                }

                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    return BadRequest("Parola yanlış.");
                }

           
            // Kullanıcıyı oturum açtırın.
            await _signInManager.SignInAsync(user, model.RememberMe);

            return Ok("Oturum açıldı.");
            }
        
    }
}
