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
    public class UserService : IUserService
    {
        private readonly IdentityDbContext _identitydbContext;
        private readonly ITokenService _tokenService;

        public UserService(IdentityDbContext dbContext, ITokenService tokenService)
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
                
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
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

        public async Task<IdentityUser> UpdateUser(string email,UserDTO model)
        {
            if (email != null)
            {
                var user = await _identitydbContext.Users.FirstOrDefaultAsync(e => e.Email ==email);
                user.UserName = model.Username;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                await _identitydbContext.SaveChangesAsync();
                return user;
            }
            else throw new Exception("User not found");
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
