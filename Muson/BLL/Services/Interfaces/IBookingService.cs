using Domain.Models;
using Domain.Models.ViewModels;

namespace BLL.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IReadOnlyCollection<Booking>> GetAllBookingAsync();
        Task AddBookingAsync(BookingViewModel booking, string userId);
        Task RemoveBookingAsync(int remBookingId);
        Task ChangeBookingAsync(BookingViewModel booking, int oldBookingId);
    }
}
