using BLL.Services;
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
    }
}
