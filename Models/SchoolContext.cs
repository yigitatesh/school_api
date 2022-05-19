using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace SchoolApi.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
    }
}