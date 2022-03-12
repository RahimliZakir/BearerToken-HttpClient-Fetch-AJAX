using BearerToken.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BearerToken.API.Models.DataContexts
{
    public class BearerDbContext : DbContext
    {
        public BearerDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder build)
        {
            base.OnModelCreating(build);

            build.Entity<Person>()
                 .Property(p => p.CreatedDate)
                 .HasDefaultValueSql("DATEADD(HOUR, 4, GETUTCDATE())");
        }
    }
}
