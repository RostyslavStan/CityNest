using Microsoft.EntityFrameworkCore;

namespace CityNest
{
    public class RealStateDbContext(DbContextOptions<RealStateDbContext> options) : DbContext(options)
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new AgentConfiguration());

            modelBuilder.Entity<Property>()
                .Property(p => p.PropertyType)
                .HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Database=RealState;Username=rostyslav;Password=31231928");
        }
    }
}
