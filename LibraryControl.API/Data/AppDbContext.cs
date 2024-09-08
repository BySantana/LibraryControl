using Microsoft.EntityFrameworkCore;
using LibraryControl.Core.Models;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LibraryControl.API.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryControl.API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext
        <
            User,
            IdentityRole<long>,
            long,
            IdentityUserClaim<long>,
            IdentityUserRole<long>,
            IdentityUserLogin<long>,
            IdentityRoleClaim<long>,
            IdentityUserToken<long>
        >(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
