using BLL.Services;
using Domain.Enum;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly UserService _userService;
        private readonly RoomService _roomService;
        private readonly SignInManager<User> _signInManager;
        private List<BookingViewModel> bookingViewModels;
        public BookingController(UserService userService, RoomService roomService,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _roomService = roomService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            /*string userId = (await _signInManager.UserManager.GetUserAsync(User)).Id;
            var user = (await _userService.FindByConditionUserAsync(x => x.Id == userId)).FirstOrDefault();
            bookingViewModels = user.Bookings.Select(x => new BookingViewModel
            {
                BookingId = x.Id,
                DateArrival = x.DateArrival,
                DateDeparture = x.DateDeparture,
                Price = x.Price,
                Room = x.Room,
                Service = x.Service,
                User = x.User

            }).ToList();
            return View(bookingViewModels);*/
            return View();
        }

        //TODO: MyBooking page

        [HttpGet]
        public async Task<IActionResult> Create(string typeRoom)
        {
            var rooms = await _roomService.GetAllAsync();
            foreach(var item in rooms)
            {
                if (item.TypeRoom.ToString() == typeRoom && item.Status.ToString() == "Available")
                {
                    BookingViewModel bookingViewModel = new BookingViewModel()
                    {
                        Room = item
                    };
                    return View(bookingViewModel);
                }
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingViewModel bookingViewModel)
        {
            var room = (await _roomService.FindByConditionAsync(x => x.TypeRoom == bookingViewModel.Room.TypeRoom)).FirstOrDefault();
            var user = await _signInManager.UserManager.GetUserAsync(User);
            bookingViewModel.Room = room;
            await _userService.AddBookingAsync(bookingViewModel, user.Id);
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int BookingId)
        {
            var booking = (await _userService.FindByConditionBookingAsync(x => x.Id == BookingId)).First();
            if (booking == null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            var bookingViewModel = new BookingViewModel()
            {
                BookingId = booking.Id,
                DateArrival = booking.DateArrival,
                DateDeparture = booking.DateDeparture,
                Price = booking.Price,
                Room = booking.Room,
                Service = booking.Service,
                User = booking.User
            };
            return View(bookingViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookingViewModel bookingViewModel, int BookingId)
        {
            if (bookingViewModel != null && BookingId != null)
            {
                bookingViewModel.Room = (await _roomService.FindByConditionAsync(x => x.TypeRoom == bookingViewModel.Room.TypeRoom)).FirstOrDefault();
                await _userService.ChangeBookingAsync(bookingViewModel, BookingId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int BookingId)
        {
            var booking = (await _userService.FindByConditionBookingAsync(x => x.Id == BookingId)).First();
            if (booking == null) return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            var bookingViewModel = new BookingViewModel()
            {
                BookingId = booking.Id,
                DateArrival = booking.DateArrival,
                DateDeparture = booking.DateDeparture,
                Price = booking.Price,
                Room = booking.Room,
                Service = booking.Service,
                User = booking.User
            };
            return View(bookingViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBooking(int BookingId)
        {
            if (ModelState.IsValid)
            {
                await _userService.RemoveBookingAsync(BookingId);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { Controller = "Home", Action = "Index" });
        }
    }
}
