using Identity_library.Data;
using Identity_library.Domain.DTOS;
using Identity_library.Domain.Helper;
using Identity_library.Domain.Interface;
using Identity_library.Domain.Models;
using Identity_library.Domain.Models.Entities;
using Identity_library.Domain.Models.User;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _identityRole;
        public UserService(IdentityDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> identityRole)
        {
            _identitydbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _identityRole = identityRole;
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


         async Task<IdentityUser> IUserService.UpdatePassword(PasswordResetModel model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.Email == model.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.OldPassword);

            if (!isPasswordValid)
            {
                throw new Exception("The old password is invalid");
            }
            if(model.OldPassword == model.NewPassword)
            { throw new Exception("The new password cannot be the same as the old one"); }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                throw new Exception("Password change failed.");
            }
            await _signInManager.SignInAsync(user, isPersistent: false);

            return null;
        }

        public async Task<IdentityRole> RoleCreate(string role)
        {
            var userrole = new IdentityRole(role);

            var result = await _identityRole.CreateAsync(userrole);

            return result.Succeeded ? userrole : throw new Exception("Could not created role");


        }
        public async Task<UserDTO> UserRole(UserDTO user, string role)
        {
            var _user = await _userManager.FindByNameAsync(user.Username);
            var _user1 = new UserDTO()
            {
                Username = _user.UserName,
                Email = _user.Email,
                PhoneNumber = _user.PhoneNumber
            };
            // Rolü bulun
            var _role = await _identityRole.FindByNameAsync(role);

            // Kullanıcıya rol atama
            var result = await _userManager.AddToRoleAsync(_user, role);

            return result.Succeeded ? _user1  : throw new Exception("Could not add user role");
        }

        public async Task<RoleControl> UserRoleControl(UserDTO user, string role)
        {
            var _user = await _userManager.FindByNameAsync(user.Username);
            var _user1 = new RoleControl()
            {
                Username = _user.UserName,
                Role = role
            };
            await _identityRole.FindByNameAsync(role);
            if (await _userManager.IsInRoleAsync(_user, role))
            {
                return _user1;
            }
            else throw new Exception($"{_user1} : User does not have this privilege"); 
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
