using BackEnd.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Data;

public partial class ToursitDbContext : DbContext
{
    public ToursitDbContext()
    {
    }

    public ToursitDbContext(DbContextOptions<ToursitDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppliedForGuide> AppliedForGuides { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<TripNotification> TripNotifications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppliedForGuide>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("AppliedForGuide");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.IdentityCard).HasColumnType("image");

            entity.HasOne(d => d.City).WithMany(p => p.AppliedForGuides)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppliedForGuide_City");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Name).IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Country");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.ToTable("Trip");

            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Trips)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_City");

            entity.HasOne(d => d.Guide).WithMany(p => p.Trips)
                .HasForeignKey(d => d.GuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_AppliedForGuide");

            entity.HasMany(d => d.Users).WithMany(p => p.Trips)
                .UsingEntity<Dictionary<string, object>>(
                    "UserTrip",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_UserTrip_User"),
                    l => l.HasOne<Trip>().WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_UserTrip_Trip"),
                    j =>
                    {
                        j.HasKey("TripId", "UserId");
                        j.ToTable("UserTrip");
                    });
        });

        modelBuilder.Entity<TripNotification>(entity =>
        {
            entity.ToTable("TripNotification");

            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Title).IsUnicode(false);

            entity.HasOne(d => d.Trip).WithMany(p => p.TripNotifications)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripNotification_Trip");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FirstName).IsUnicode(false);
            entity.Property(e => e.Gender).IsUnicode(false);
            entity.Property(e => e.LastName).IsUnicode(false);
            entity.Property(e => e.PhoneNumber).IsUnicode(false);
            entity.Property(e => e.ProfilePicture).HasColumnType("image");
            entity.Property(e => e.RefreshToken).IsUnicode(false);
            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => new { e.NotificationId, e.UserId });

            entity.ToTable("UserNotification");

            entity.HasOne(d => d.Notification).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserNotification_TripNotification");

            entity.HasOne(d => d.User).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserNotification_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
