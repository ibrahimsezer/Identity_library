using Identity_library.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity_library.Data
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser> 
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContext) : base(dbContext) { }
        
        public DbSet<UserAddress> UserAddresses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=SEZER;Database=IdentityExampleDb;Integrated Security=True;Trust Server Certificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
