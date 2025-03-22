using Grades.Models;
using Microsoft.EntityFrameworkCore;

namespace Grades.Data
{
    public class MySQLDbContext : DbContext
    {
        public MySQLDbContext(
            DbContextOptions<MySQLDbContext> options) : base(options) { }
        public DbSet<Subject> subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
