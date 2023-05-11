using BLL.Services;
using Microsoft.AspNetCore.Identity;

namespace Muson.Infrastructure
{
    public class Configuration
    {
        public static void ConfigurationService(IdentityBuilder builder)
        {
            builder.Services.AddTransient<RoomService>();
            builder.Services.AddTransient<UserService>();
        }
    }
}
