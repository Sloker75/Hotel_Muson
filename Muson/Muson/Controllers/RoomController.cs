using BLL.Services;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _roomService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {
                await _roomService.AddRoomAsync(roomVM);
                return View("Index", await _roomService.GetAllAsync());
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int editRoomId)
        {
            Room room = (await _roomService.FindByConditionAsync(x => x.Id == editRoomId)).First();
            if (room == null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            /*RoomViewModel roomVM = new RoomViewModel()
            {
                RoomNumber = room.RoomNumber,
                CountRoom = room.CountRoom,
                Floor = room.Floor,
                TypeRoom = room.TypeRoom,
                Status = room.Status
            };*/
            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomViewModel roomVM, int Id)
        {
            if (roomVM != null && Id != null)
            {
                await _roomService.ChangeRoomAsync(roomVM, Id);
                return View("Index", await _roomService.GetAllAsync());
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int remRoomId)
        {
            Room room = (await _roomService.FindByConditionAsync(x => x.Id == remRoomId)).First();
            if (room == null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            /*RoomViewModel roomVM = new RoomViewModel()
            {
                RoomNumber = room.RoomNumber,
                CountRoom = room.CountRoom,
                Floor = room.Floor,
                TypeRoom = room.TypeRoom,
                Status = room.Status
            };*/
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRoom(int Id)
        {
            if (ModelState.IsValid)
            {
                await _roomService.RemoveRoomAsync(Id);
                return View("Index", await _roomService.GetAllAsync());
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }
    }
}
