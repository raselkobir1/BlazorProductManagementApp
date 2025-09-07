using BlazorProducts.Server.Context.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorProducts.Server.Context
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Product> Products { get; set; }
    }
}
