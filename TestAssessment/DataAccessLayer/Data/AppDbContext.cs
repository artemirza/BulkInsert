using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TripData> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=WIN-9977J4MG98L;Database=TestDb;User Id=sa;Password=12345;TrustServerCertificate=True;"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripData>()
                .HasKey(t => new { t.tpep_pickup_datetime, t.tpep_dropoff_datetime, t.passenger_count });
        }
    }
}
