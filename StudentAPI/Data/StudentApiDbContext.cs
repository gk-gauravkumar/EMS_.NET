using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Data
{
    public class StudentApiDbContext : DbContext
    {
        public StudentApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> students { get; set; }
    }
}
