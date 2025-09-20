using Microsoft.EntityFrameworkCore;
using RenCar.Domain.Entities;

namespace RenCar.DataAccess.Context;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-F80D8E5;Database=RentCar_db;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
    }

    public DbSet<BookDetails> BookDetails { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<CarLocation> CarLocations { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarDetails> CarDetails { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<RentCompany> RentCompanies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(book =>
        {
            book.ToTable("Bookings");

            book.HasKey(b => b.Id);

            book.Property(b => b.TotalPrice)
                  .HasColumnType("decimal(18,2)");

            book.HasOne(b => b.User)
                  .WithMany(u => u.Bookings)
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<BookDetails>(b =>
        {
            b.HasKey(x => x.Id);

            b.HasOne(x => x.Booking)
                .WithOne(x => x.BookDetails)
                .HasForeignKey<BookDetails>(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.PickUpLocation)
                .WithMany()
                .HasForeignKey(x => x.PickUpLocationId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne(x => x.DropUpLocation)
                .WithMany()
                .HasForeignKey(x => x.DropUpLocationId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<CarLocation>()
            .HasIndex(cl => new { cl.CarId, cl.LocationId })
            .IsUnique();

        modelBuilder.Entity<CarLocation>(l =>
        {
            l.HasKey(cl => cl.Id);

            l.HasOne(cl => cl.Car)
             .WithMany(c => c.DropOffLocations)
             .HasForeignKey(cl => cl.CarId)
             .OnDelete(DeleteBehavior.Cascade);

            l.HasOne(cl => cl.Location)
             .WithMany(loc => loc.CarLocations)
             .HasForeignKey(cl => cl.LocationId)
             .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
