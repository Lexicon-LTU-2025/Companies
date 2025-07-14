using Companies.Infractructure.Data.Configurations;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Companies.Infractructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee, IdentityRole, string>
    {
        public DbSet<Company> Companies => Set<Company>();
      // public DbSet<Employee> Users { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        //    modelBuilder.ApplyConfiguration(new EmployeeConfigurations());
        //}

    }
}
