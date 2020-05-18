using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniPool.Model
{
    public class UniPoolContext : DbContext
    {
        public UniPoolContext(DbContextOptions<UniPoolContext> options)
          : base(options)
            { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trip>()
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)

            modelBuilder.Entity<Student>()
                .Property(p => p.AccountType)
                .HasConversion(
                    p => p.Value,
                    p => AccountType.FromValue(p));

            modelBuilder.Entity<Trip>()
                .Property(p => p.Status)
                .HasConversion(
                    p => p.Value,
                    p => TripStatus.FromValue(p));

            modelBuilder.Entity<StudentTrip>().HasKey(x => new { x.TripId, x.StudentId });
        }
    }
}
