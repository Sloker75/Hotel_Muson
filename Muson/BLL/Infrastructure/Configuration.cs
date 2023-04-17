using DLL.Context;
using DLL.Repository;
using Microsoft.AspNetCore.Identity;
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
        }
    }
}
