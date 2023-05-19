using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DLL.Context
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new AppRole("Admin", Guid.NewGuid().ToString()));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "VladBuriloDeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {

                    var newAdminUser = new User()
                    {
                        UserName = adminUserEmail,
                        Name = "Vladyslav",
                        Email = adminUserEmail,
                        PhoneNumber = "2049526383",
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");

                    var employee = new Employee()
                    {
                        Position = "Admin",
                        Salary = 1000,
                        Address = new Address()
                        {
                            Country = "Canada",
                            City = "Charlotte",
                            Street = "123 Main St"
                        },
                        User = newAdminUser
                    };

                    newAdminUser.Employee = employee;
                    await userManager.UpdateAsync(newAdminUser);

                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
            }
        }
    }
}
