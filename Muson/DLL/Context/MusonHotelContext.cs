using Domain.Enum;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DLL.Context
{
    public class MusonHotelContext : IdentityDbContext<User>
    {
        public MusonHotelContext(DbContextOptions<MusonHotelContext> options) : base(options)
        {

        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<string>>().HasNoKey();

            builder
           .Entity<Room>()
           .Property(x => x.Status)
           .HasConversion(new EnumToStringConverter<RoomStatus>());

            builder
           .Entity<Room>()
           .Property(x => x.TypeRoom)
           .HasConversion(new EnumToStringConverter<TypeRoom>());
        }*/
    }
}
