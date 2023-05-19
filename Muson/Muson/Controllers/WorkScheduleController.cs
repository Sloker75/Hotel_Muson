using BLL.Services;
using BLL.Services.Interfaces;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    [Authorize]
    public class WorkScheduleController : Controller
    {
        private readonly EmployeeService _employeeService;
        public WorkScheduleController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var workScheduleVM = new WorkScheduleViewModel();
            return View(workScheduleVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkScheduleViewModel scheduleViewModel, int employeeId)
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int scheduleId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkScheduleViewModel scheduleViewModel, int oldScheduleId)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int scheduleId)
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.RemoveWorkScheduleAsync(scheduleId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

    }
}
