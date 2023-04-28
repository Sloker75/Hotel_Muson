using BLL.Services;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomService _roomService;
        private List<RoomViewModel> roomViewModels;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }
        public async Task<IActionResult> Index()
        {
            var roomModels = await _roomService.GetAllAsync();
            roomViewModels = roomModels.Select(rm => new RoomViewModel
            {
                RoomId = rm.Id,
                RoomNumber = rm.RoomNumber,
                CountRoom = rm.CountRoom,
                Floor = rm.Floor,
                TypeRoom = rm.TypeRoom,
                Status = rm.Status,
                Bookings = rm.Bookings

            }).ToList();

            return View(roomViewModels);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {
                await _roomService.AddRoomAsync(roomVM);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int RoomId)
        {
            Room room = (await _roomService.FindByConditionAsync(x => x.Id == RoomId)).First();
            if (room == null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            RoomViewModel roomVM = new RoomViewModel()
            {
                RoomId = room.Id,
                RoomNumber = room.RoomNumber,
                CountRoom = room.CountRoom,
                Floor = room.Floor,
                TypeRoom = room.TypeRoom,
                Status = room.Status,
                Bookings = room.Bookings
            };
            return View(roomVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoomViewModel roomVM, int RoomId)
        {
            if (roomVM != null && RoomId != null)
            {
                await _roomService.ChangeRoomAsync(roomVM, RoomId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int RoomId)
        {
            Room room = (await _roomService.FindByConditionAsync(x => x.Id == RoomId)).First();
            if (room == null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            RoomViewModel roomVM = new RoomViewModel()
            {
                RoomId = room.Id,
                RoomNumber = room.RoomNumber,
                CountRoom = room.CountRoom,
                Floor = room.Floor,
                TypeRoom = room.TypeRoom,
                Status = room.Status,
                Bookings = room.Bookings
            };
            return View(roomVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRoom(int RoomId)
        {
            if (ModelState.IsValid)
            {
                await _roomService.RemoveRoomAsync(RoomId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }
    }
}
