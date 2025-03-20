﻿using Microsoft.EntityFrameworkCore;

namespace CityNest
{
    public class RealStateDbContext(DbContextOptions<RealStateDbContext> options) : DbContext(options)
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>()
                .Property(p => p.Price)
                .HasPrecision(18, 4);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost;Database=RealState;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
