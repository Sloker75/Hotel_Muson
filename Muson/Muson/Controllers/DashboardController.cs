using BLL.Services;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        public DashboardController(UserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ICollection<ExtraService> ExtraServices = (await _userService.FindByConditionExtraServiceAsync(x => x.UserId == user.Id)).ToList();
            ICollection<Booking> Bookings = (await _userService.FindByConditionBookingAsync(x => x.UserId == user.Id)).ToList();
            var dashboardViewModel = new DashBoardViewModel()
            {
                Name = user.Name,
                Email = user.Email,
                EmployeeId = user.EmployeeId,
                Employee = user.Employee,
                ExtraServices = ExtraServices,
                Bookings = Bookings
            };
            return View(dashboardViewModel);
        }
    }
}
