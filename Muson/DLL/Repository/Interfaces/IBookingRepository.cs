using Domain.Models;

namespace DLL.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateBookingAsync(Booking booking, string userId);
        Task ChangeBookingAsync(Booking newBooking, int oldBookingId);
        Task DeleteBookingAsync(int remBookingId);
    }
}
