using Identity_library.Interface;
using Identity_library.Models;
using Identity_library.Models.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Identity_library.Service
{
    public class ProductService : IProductService
    {
        private readonly IdentityDbContext _identitydbContext;

        public ProductService(IdentityDbContext dbContext)
        {
            _identitydbContext = dbContext;
        }

        public IdentityUser GetProductByPnumber(string pnumber)
        {
            if (pnumber != null)
            {
                return _identitydbContext.Users.FirstOrDefault(p => p.PhoneNumber == pnumber);
                 
            }
            else { throw new Exception("Phone Number not found!"); }
        }
    }
}
