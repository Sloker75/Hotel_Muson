using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        /*public class UserConfiguration : EntityTypeConfiguration<User>
        {
            public UserConfiguration()
            {
                HasOptional(u => u.Employee)
                    .WithRequired(e => e.User)
                    .WillCascadeOnDelete(true);
            }
        }

        public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
        {
            public EmployeeConfiguration()
            {
                HasRequired(e => e.User)
                    .WithOptional(u => u.Employee)
                    .WillCascadeOnDelete(false);
            }
        }*/
    }
}
