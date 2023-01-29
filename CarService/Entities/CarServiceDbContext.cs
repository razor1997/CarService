using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class CarServiceDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarServiceDb;Trusted_Connection=True;";
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<CarMarket> CarMarkets { get; set; }
        public DbSet<CarMarketAddress> CarMarketAddresses { get; set; }
        public DbSet<CarPart> CarParts { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(r => r.Mark)
                .HasMaxLength(25);
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();
            //modelBuilder.Entity<User>()
            //    .Property(u => u.SurName)
            //    .HasMaxLength(50);
            modelBuilder.Entity<CarMarket>()
                .Property(cm => cm.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<CarPart>()
                .Property(cp => cp.Category)
                .IsRequired();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
