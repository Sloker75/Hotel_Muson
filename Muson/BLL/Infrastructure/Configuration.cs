using DLL.Context;
using DLL.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure
{
    public class Configuration
    {

        public static void ConfigurationService(IServiceCollection serviceCollection, string connectionString, IdentityBuilder builder)
        {
            serviceCollection.AddDbContext<MusonHotelContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Muson")));

            

            builder.AddEntityFrameworkStores<MusonHotelContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddTransient<RoomRepository>();
            builder.Services.AddTransient<BookingRepository>();
            builder.Services.AddTransient<ExtraServiceRepository>();
            builder.Services.AddTransient<EmployeeRepository>();
            builder.Services.AddTransient<WorkScheduleRepository>();
            builder.Services.AddTransient<CommentRepository>();
            builder.Services.AddTransient<RoleRepository>();
            builder.Services.AddTransient<UserRepository>();

        }
    }
}
