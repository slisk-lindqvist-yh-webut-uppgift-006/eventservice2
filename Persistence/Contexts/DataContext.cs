using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
    // public DbSet<EventLocationEntity> Locations { get; set; }
    public DbSet<PackageEntity> Packages { get; set; }
    public DbSet<EventPackageEntity> EventsPackages { get; set; }

    #region ChatGPT Advice (Because I wanted to have Unique Index for locations, to save DB storage.)

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //
        //     modelBuilder.Entity<EventEntity>()
        //         .HasOne(e => e.Location)
        //         .WithMany()
        //         .HasForeignKey(e => e.LocationId);
        //     
        //     modelBuilder.Entity<EventLocationEntity>(entity =>
        //     {
        //         entity.HasIndex(e => new { e.StreetName, e.PostalCode, e.City, e.Country })
        //             .IsUnique()
        //             .HasDatabaseName("UX_EventLocation_Street_Postal_City_Country");
        //     });
        // }

    #endregion
}