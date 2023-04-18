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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {
                await _roomService.AddRoomAsync(roomVM);
                return View("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int roomId)
        {
            Room room = (await _roomService.FindByConditionAsync(x => x.Id == roomId)).First();
            if(room != null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            RoomViewModel roomVM = new RoomViewModel()
            {
                RoomNumber = room.RoomNumber,
                CountRoom = room.CountRoom,
                Floor = room.Floor,
                TypeRoom = room.TypeRoom,
                Status = room.Status
            };
            return View(roomVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomViewModel roomVM, int oldRoomId)
        {
            if (roomVM != null && oldRoomId != null)
            {
                await _roomService.ChangeRoomAsync(roomVM, oldRoomId);
                return View("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int remRoomId)
        {
            Room room = (await _roomService.FindByConditionAsync(x => x.Id == remRoomId)).First();
            if (room != null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            RoomViewModel roomVM = new RoomViewModel()
            {
                RoomNumber = room.RoomNumber,
                CountRoom = room.CountRoom,
                Floor = room.Floor,
                TypeRoom = room.TypeRoom,
                Status = room.Status
            };
            return View(roomVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRoom(int remRoomId)
        {
            if (ModelState.IsValid)
            {
                await _roomService.RemoveRoomAsync(remRoomId);
                return View("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }
    }
}
