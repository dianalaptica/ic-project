using BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.TouristGuide)
            .WithMany()
            .HasForeignKey("TouristGuideId")
            .IsRequired();
        
        // modelBuilder.Entity<Trip>()
        //     .HasMany(t => t.Tourists)
        //     .WithMany(u => u.Trips)
        //     .UsingEntity<Dictionary<string, object>>(
        //         "TripUser",
        //         j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
        //         j => j.HasOne<Trip>().WithMany().HasForeignKey("TripId"),
        //         j =>
        //         {
        //             j.HasKey("TripId", "UserId");
        //         }
        //     );
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
}