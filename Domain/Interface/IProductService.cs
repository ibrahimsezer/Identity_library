using Identity_library.Domain.DTOS;
using Identity_library.Domain.Models;
using Microsoft.AspNetCore.Identity;
using SharedLibrary;

namespace Identity_library.Domain.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IdentityUser> GetByNumber(string pnumber);
        Task<Response<string>> Login(LoginModel model);
        Task<IdentityUser> DeleteUser(string pnumber);
    }
}
