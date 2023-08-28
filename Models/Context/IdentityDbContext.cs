using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity_library.Models.Context
{
    public class IdentityDbContext : IdentityDbContext<User.User>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContext) : base(dbContext) { }

    }
}
