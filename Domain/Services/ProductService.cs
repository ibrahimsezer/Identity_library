using Identity_library.Data;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Identity_library.Domain.Models.Entities;
using Identity_library.Domain.Models.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity_library.Domain.Service
{
    public class ProductService : IProductService
    {
        private readonly IdentityDbContext _identitydbContext;
        private readonly ITokenService _tokenService;

        public ProductService(IdentityDbContext dbContext, ITokenService tokenService)
        {
            _identitydbContext = dbContext;
            _tokenService = tokenService;
        }

        public IdentityUser GetProductByPnumber(string pnumber)
        {
            if (pnumber != null)
            {
                return _identitydbContext.Users.FirstOrDefault(p => p.PhoneNumber == pnumber);

            }
            else { throw new Exception("Phone Number not found!"); }
        }

        public ApiResponse<string> Login(LoginModel model)
        {
            var user = _identitydbContext.Users.FirstOrDefaultAsync(p => p.UserName == model.UserName);
            var claims = new[]
            {
        new Claim("email", model.UserName),
        new Claim("userid", user.Id.ToString())
    };

            //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey123456789012345678901234"));
            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    claims: claims,
            //    signingCredentials: signingCredentials,
            //    expires: DateTime.Now.AddHours(12));

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenString = tokenHandler.WriteToken(token);
            //var datatoken = $"{tokenString}    |   Email :{model.UserName}";

            var token = _tokenService.GenerateToken(model.UserName, claims);
            var datatoken = $"{token}    |   Email :{model.UserName}";
            return new ApiResponse<string>
            {
                Success = true,
                Data = datatoken
            };
        }

    }
}
