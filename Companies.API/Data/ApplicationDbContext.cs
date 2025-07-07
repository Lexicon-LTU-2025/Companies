using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Companies.API.Entities;

namespace Companies.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; } = default!;
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
