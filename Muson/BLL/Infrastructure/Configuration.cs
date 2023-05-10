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

            

            builder.AddEntityFrameworkStores<MusonHotelContext>();

            builder.Services.AddTransient<RoomRepository>();
            builder.Services.AddTransient<BookingRepository>();
            builder.Services.AddTransient<ExtraServiceRepository>();
            builder.Services.AddTransient<EmployeeRepository>();
            builder.Services.AddTransient<RoleManager<IdentityRole>>();
            //builder.Services.AddTransient<IUserStore<User>, UserStore<User, IdentityRole, MusonHotelContext, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>();

            builder.Services.AddTransient<UserRepository>();

        }
    }
}
