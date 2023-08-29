using Identity_library.Models;
using Identity_library.Models.Context;
using Microsoft.AspNetCore.Identity;

namespace Identity_library.Interface
{
    public interface IProductService
    {
       IdentityUser GetProductByPnumber(string pnumber);
    }
}
