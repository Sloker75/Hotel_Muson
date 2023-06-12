using BLL.Services;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            var employeeRegVM = new UserRegistrationViewModel();
            return View(employeeRegVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(UserRegistrationViewModel userRegVM)
        {
            userRegVM.Employee.Position = userRegVM.Role;
            if (await _userService.RegistrationAsync(userRegVM)) return RedirectToAction("Login");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string email)
        {
            var user = (await _userService.FindByConditionUserAsync(x => x.Email == email)).First();
            if (user == null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            var userViewModel = new UserViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Employee = user.Employee
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userViewModel, string Email)
        {
            var user = (await _userService.FindByConditionUserAsync(x => x.Email == Email)).FirstOrDefault();
            if (userViewModel != null && Email != null && user != null)
            {
                await _userService.ChangeUserAsync(userViewModel, Email);
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }
    }
}
