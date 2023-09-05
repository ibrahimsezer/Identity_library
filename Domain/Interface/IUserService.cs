using Identity_library.Domain.DTOS;
using Identity_library.Domain.Models;
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
    }
}
