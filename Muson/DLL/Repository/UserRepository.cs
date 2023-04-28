﻿using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace DLL.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public UserRepository(MusonHotelContext _musonHotelContext,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager, 
            SignInManager<User> signInManager) : base(_musonHotelContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task ChangeUserAsync(User newUser, string oldUserId)
        {
            var oldUser = Entities.Find(oldUserId);

            oldUser.Name = newUser.Name;
            oldUser.Email = newUser.Email;
            oldUser.PhoneNumber = newUser.PhoneNumber;
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
                await _roleManager.CreateAsync(new IdentityRole(userRegistrationVM.Role));
            else
                await _userManager.AddToRoleAsync(user, userRegistrationVM.Role);

            return true;
        }
    }
}