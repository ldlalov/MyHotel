using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hotel.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Building> Hotels { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<RoomGuest> RoomsGuests { get; set; }
        //public DbSet<UsersHotels> UsersHotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomGuest>()
                .HasKey(rg => new { rg.RoomId, rg.GuestId });

            modelBuilder.Entity<Room>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,5)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Sum)
                .HasColumnType("decimal(18,5)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Discount)
                .HasColumnType("decimal(18,5)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Vat)
                .HasColumnType("decimal(18,5)");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Hotel)
                .WithMany()
                .HasForeignKey(b => b.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}