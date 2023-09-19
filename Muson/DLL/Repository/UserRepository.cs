using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

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

        public async Task ChangeUserAsync(UserViewModel newUser, string oldUserEmail)
        {
            var oldUser = Entities.FirstOrDefault(x => x.Email == oldUserEmail);
            var userRole = await _userManager.GetRolesAsync(oldUser);

            oldUser.Name = newUser.Name;
            oldUser.Surname = newUser.Surname;
            oldUser.BirthDate = newUser.BirthDate;
            oldUser.Email = newUser.Email;
            oldUser.PhoneNumber = newUser.PhoneNumber;
            oldUser.UserName = newUser.Email;

            if (userRole.First() != "User")
            {
                oldUser.Employee.Salary = newUser.Employee.Salary;
                oldUser.Employee.Address.Country = newUser.Employee.Address.Country;
                oldUser.Employee.Address.City = newUser.Employee.Address.City;
                oldUser.Employee.Address.Street = newUser.Employee.Address.Street;
            } 

            base._musonHotelContext.Entry(oldUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(string remUserId)
        {
            Entities.Remove(Entities.Find(remUserId));
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task ChangeUserPasswordAsync(User user, string currentPassword, string newPassword)
            => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        

        public async override Task<IReadOnlyCollection<User>> FindByConditionAsync(Expression<Func<User, bool>> predicat)
             => await this.Entities.Include(x => x.Bookings).ThenInclude(x => x.Room)
            .Include(x => x.Bookings).ThenInclude(x => x.Service)
            .Include(x => x.Employee).ThenInclude(x => x.Address)
            .Include(x => x.Employee).ThenInclude(x => x.WorkSchedules)
            .Include(x => x.ExtraServices).Where(predicat).ToListAsync().ConfigureAwait(false);

        public async override Task<IReadOnlyCollection<User>> GetAllAsync()
            => await this.Entities.Include(x => x.Bookings).ThenInclude(x => x.Room)
            .Include(x => x.Bookings).ThenInclude(x => x.Service)
            .Include(x => x.Employee).ThenInclude(x => x.Address)
            .Include(x => x.Employee).ThenInclude(x => x.WorkSchedules)
            .Include(x => x.ExtraServices).ToListAsync().ConfigureAwait(false);


        public async Task<bool> RegistrationAsync(UserRegistrationViewModel userRegistrationVM)
        {
            var userExists = await _userManager.FindByEmailAsync(userRegistrationVM.Email);
            if (userExists != null) return false;
            User user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = userRegistrationVM.Name,
                Surname = userRegistrationVM.Surname,
                BirthDate = userRegistrationVM.BirthDate,
                Email = userRegistrationVM.Email,
                UserName = userRegistrationVM.Email,
                PhoneNumber = userRegistrationVM.PhoneNumber,
                Employee = userRegistrationVM.Employee,
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
