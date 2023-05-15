using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UserRepository(MusonHotelContext _musonHotelContext,
            UserManager<User> userManager, RoleManager<AppRole> roleManager)
            : base(_musonHotelContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task ChangeUserAsync(User newUser, string oldUserId)
        {
            var oldUser = Entities.Find(oldUserId);

            oldUser.Name = newUser.Name;
            oldUser.Email = newUser.Email;
            oldUser.PhoneNumber = newUser.PhoneNumber;
            oldUser.UserName = newUser.UserName;
            oldUser.EmployeeId = newUser.Employee.Id;
            oldUser.Employee = newUser.Employee;
            oldUser.Bookings = newUser.Bookings;
            oldUser.ExtraServices = newUser.ExtraServices;

            base._musonHotelContext.Entry(oldUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(string remUserId)
        {
            Entities.Remove(Entities.Find(remUserId));
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task ChangeUserPasswordAsync(User user, string currentPassword, string newPassword)
        {
            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
        

        public async override Task<IReadOnlyCollection<User>> FindByConditionAsync(Expression<Func<User, bool>> predicat)
             => await this.Entities.Include(x => x.Bookings).ThenInclude(x => x.Room)
            .Include(x => x.Bookings).ThenInclude(x => x.Service)
            .Include(x => x.Employee).Include(x => x.ExtraServices).Where(predicat).ToListAsync().ConfigureAwait(false);

        public async override Task<IReadOnlyCollection<User>> GetAllAsync()
            => await this.Entities.Include(x => x.Bookings).ThenInclude(x => x.Room)
            .Include(x => x.Bookings).ThenInclude(x => x.Service)
            .Include(x => x.Employee).Include(x => x.ExtraServices).ToListAsync().ConfigureAwait(false);


        public async Task<bool> RegistrationAsync(UserRegistrationViewModel userRegistrationVM)
        {
            var userExists = await _userManager.FindByEmailAsync(userRegistrationVM.Email);
            if (userExists != null) return false;
            User user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = userRegistrationVM.Name,
                Email = userRegistrationVM.Email,
                UserName = userRegistrationVM.UserName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, userRegistrationVM.Password);
            if (!result.Succeeded) return false;


            if (!await _roleManager.RoleExistsAsync(userRegistrationVM.Role))
            {
                string roleId = Guid.NewGuid().ToString();
                await _roleManager.CreateAsync(new AppRole(userRegistrationVM.Role, roleId));
            }
            if (await _roleManager.RoleExistsAsync(userRegistrationVM.Role))
                await _userManager.AddToRoleAsync(user, userRegistrationVM.Role);

            return true;
        }
    }
}
