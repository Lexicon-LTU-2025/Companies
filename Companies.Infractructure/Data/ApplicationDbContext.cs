using Companies.Infractructure.Data.Configurations;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infractructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfigurations());
        }

    }
}
