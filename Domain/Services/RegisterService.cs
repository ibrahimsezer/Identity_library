using Identity_library.Data;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using SharedLibrary;

namespace Identity_library.Domain.Services
{
    public class RegisterService :IRegisterService
    {
        private readonly IdentityDbContext _identitydbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public RegisterService(IdentityDbContext identitydbContext,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _identitydbContext = identitydbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        async Task<Response<RegisterModel>> IRegisterService.RegisterAsyncService(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var registerResult = new Response<RegisterModel>
                {
                    Data = new RegisterModel
                    {
                        Email = user.UserName,
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
    }
}
