using Identity_library.Domain.Models;
using Identity_library.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity_library.Domain.Interface
{
    public interface IProductService
    {
        IdentityUser GetProductByPnumber(string pnumber);
        ApiResponse<string> Login(LoginModel model);
    }
}
