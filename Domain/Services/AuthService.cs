using Identity_library.Data;
using Identity_library.Domain.Helper;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using System.Security.Claims;

namespace Identity_library.Domain.Services
{
    public class AuthService :IAuthService
    {
        private readonly IdentityDbContext _identitydbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(IdentityDbContext identitydbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _identitydbContext = identitydbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        async Task<Response<RegisterModel>> IAuthService.RegisterAsyncService(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email, PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var registerResult = new Response<RegisterModel>
                {
                    Data = new RegisterModel
                    {
                        Username = user.UserName,
                        Email = user.Email,
                        Password = model.Password,
                        PhoneNumber = user.PhoneNumber
                    },
                    IsSuccess = true
                };
                return registerResult;
            }
            else
            {
                // Kayıt başarısız ise hata bilgilerini döndüren failed metodu
                //Hata için ayrı mesajlar fonksiyona yazılıp gönderilebilir
                var errorResponse = Response<RegisterModel>.Failed();
                var errors = result.Errors.Select(e => e.Description);
                
                errorResponse.Errors = errors;
                return errorResponse;
            }
        }
        public async Task<Response<string>> Login(LoginModel model)
        {
            var user = await _identitydbContext.Users.FirstOrDefaultAsync(p => p.UserName == model.UserName);
            var claims = new[]
            {
            new Claim("email", model.UserName),
            new Claim("userid", user.Id.ToString())
            };
            var token = _tokenService.GenerateToken(model.UserName, claims);
            var datatoken = $"{token}    |   Email :{model.UserName}";
            var resp = new Response<string>
            {
                IsSuccess = true,
                Data = datatoken
            };
            return resp;
        }
    }
}
