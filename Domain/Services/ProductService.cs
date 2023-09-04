using Identity_library.Data;
using Identity_library.Domain.DTOS;
using Identity_library.Domain.Helper;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using System.Security.Claims;

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



        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {

            var data = await _identitydbContext.Users.ToListAsync();
            // Tüm verileri çekmek için DbContext'i kullanın
            var userDTOs = data.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Phonenumber = user.PhoneNumber
            }).ToList();
            return userDTOs;

        }

        public async Task<IdentityUser> GetByNumber(string pnumber)
        {
            if (pnumber != null)
            {
                return _identitydbContext.Users.FirstOrDefault(p => p.PhoneNumber == pnumber);

            }
            else { throw new Exception("Phone Number not found!"); }
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
        public async Task<IdentityUser> DeleteUser(string pnumber)
        {
            if (pnumber != null)
            {
                var user = await _identitydbContext.Users.FirstOrDefaultAsync(p=>p.PhoneNumber == pnumber);
                _identitydbContext.Remove(user);
                await _identitydbContext.SaveChangesAsync();
                return null;
            }
            else { throw new Exception("Phone number not found"); }
        }

        //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey123456789012345678901234"));
        //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        //var token = new JwtSecurityToken(
        //    claims: claims,
        //    signingCredentials: signingCredentials,
        //    expires: DateTime.Now.AddHours(12));

        //var tokenHandler = new JwtSecurityTokenHandler();
        //var tokenString = tokenHandler.WriteToken(token);
        //var datatoken = $"{tokenString}    |   Email :{model.UserName}";


    }
}
