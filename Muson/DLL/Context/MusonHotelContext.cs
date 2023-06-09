﻿using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DLL.Context
{
    public class MusonHotelContext : IdentityDbContext<User, AppRole, string>
    {
        public MusonHotelContext(DbContextOptions<MusonHotelContext> options) : base(options)
        {

        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }

    }
}
