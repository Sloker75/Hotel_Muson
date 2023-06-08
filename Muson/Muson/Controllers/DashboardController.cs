using BLL.Services;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly EmployeeService _employeeService;
        public DashboardController(UserService userService, UserManager<User> userManager,
            EmployeeService employeeService)
        {
            _userService = userService;
            _userManager = userManager;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ICollection<ExtraService> ExtraServices = (await _userService.FindByConditionExtraServiceAsync(x => x.UserId == user.Id)).ToList();
            ICollection<Booking> Bookings = (await _userService.FindByConditionBookingAsync(x => x.UserId == user.Id)).ToList();
            var Employee = (await _employeeService.FindByConditionEmployeeAsync(x => x.UserId == user.Id)).FirstOrDefault();
            var dashboardViewModel = new DashBoardViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmployeeId = user.EmployeeId,
                Employee = Employee,
                ExtraServices = ExtraServices,
                Bookings = Bookings
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> PaymentData()
        {
            var user = await _userManager.GetUserAsync(User);
            var Employee = (await _employeeService.FindByConditionEmployeeAsync(x => x.UserId == user.Id)).FirstOrDefault();
            var dashboardViewModel = new DashBoardViewModel()
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmployeeId = user.EmployeeId,
                Employee = Employee,
            };
            return View(dashboardViewModel);
        }
    }
}
