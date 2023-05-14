using Domain.Models;
using Domain.Models.ViewModels;

namespace DLL.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBookingAsync(Booking booking, string userId);
        Task ChangeBookingAsync(BookingViewModel newBooking, int oldBookingId);
        Task DeleteBookingAsync(int remBookingId);
    }
}
