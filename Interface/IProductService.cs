using Identity_library.Models;
using Identity_library.Models.Context;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Identity_library.Interface
{
    public interface IProductService
    {
       IdentityUser GetProductByPnumber(string pnumber);
        ApiResponse<string> Login(LoginModel model);
    }
}
