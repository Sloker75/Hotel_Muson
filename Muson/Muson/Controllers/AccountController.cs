using BLL.Services;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Muson.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserService userService, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var loginVM = new UserLoginViewModel();
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginVM)
        {
            var user = await _userManager.FindByEmailAsync(userLoginVM.Email);
            if(user == null)
                return View("Error");
            if(!await _userManager.CheckPasswordAsync(user, userLoginVM.Password))
                return View("Error");
            var signInResult = await _signInManager.PasswordSignInAsync(user, userLoginVM.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach (var userRole in userRoles)
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                return View();
            }
            else
                return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            var regVM = new UserRegistrationViewModel();
            return View(regVM);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistrationViewModel userRegVM)
        {
            if (!ModelState.IsValid)
                return View(userRegVM);
            if(await _userService.RegistrationAsync(userRegVM))
                return RedirectToAction("Index", "Home");
            return View(userRegVM);

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
