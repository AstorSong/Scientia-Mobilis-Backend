using Microsoft.EntityFrameworkCore;
using ScientiaMobilis.Models;

namespace ScientiaMobilis.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<EBook> EBooks { get; set; }
    }
}
