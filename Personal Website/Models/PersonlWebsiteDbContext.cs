using Microsoft.EntityFrameworkCore;

namespace Personal_Website.Models
{
    public class PersonlWebsiteDbContext : DbContext
    {
        public PersonlWebsiteDbContext(DbContextOptions<PersonlWebsiteDbContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<IPRange> iPRanges { get; set; }
    }
}
