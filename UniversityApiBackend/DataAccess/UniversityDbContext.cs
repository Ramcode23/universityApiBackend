
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDbContext: IdentityDbContext
    {

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options):base(options)
        {

        }

        //TODO: Add DbSets (Tables our Database)

         public DbSet<User>?Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Student>? Students { get; set; }

    }
}
