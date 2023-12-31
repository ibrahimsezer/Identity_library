﻿using Identity_library.Domain.DTOS;
using Identity_library.Domain.Models;
using Identity_library.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using SharedLibrary;

namespace Identity_library.Domain.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IdentityUser> GetByNumber(string pnumber);
        Task<IdentityUser> DeleteUser(string pnumber);
        Task<IdentityUser> UpdateUser(string email,UserDTO model);
        Task<IdentityUser> UpdatePassword(PasswordResetModel model);
        Task<IdentityRole> RoleCreate(string role);
        Task<UserDTO> UserRole(UserDTO user,string role);
        Task<RoleControl> UserRoleControl(UserDTO user, string role);
    }
}
