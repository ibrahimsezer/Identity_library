using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Identity_library.Domain.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Identity_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {

            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Response<RegisterModel> result = await _authService.RegisterAsyncService(model);

                if (result.IsSuccess)
                {
                    return Ok(new { Message = "Registration successful" });
                }
                else
                {
                    return BadRequest(result.Errors);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
        [HttpPost("token_login")]
        public async Task<IActionResult> Login_Token([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var loginResponse = await _authService.Login(model);
            if (loginResponse.IsSuccess)
            {
                return Ok(loginResponse);
            }

            return BadRequest(loginResponse);
        }

    }
}