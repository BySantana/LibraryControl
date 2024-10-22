using Microsoft.EntityFrameworkCore;
using LibraryControl.Core.Models;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LibraryControl.API.Models;
using Microsoft.AspNetCore.Identity;
using LibraryControl.Core.Models.Reports;

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
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<BooksByCategory> BooksByCategories { get; set; } = null!;
        public DbSet<AvgBooksByMonth> AvgBooksByMonths { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<BooksByCategory>()
                .HasNoKey()
                .ToView("vwGetBooksByCategory");
            
            modelBuilder.Entity<AvgBooksByMonth>()
                .HasNoKey()
                .ToView("vwGetAvgBooksByMonth");
        }
    }
}
