using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IReadOnlyCollection<Booking>> GetAllBookingAsync();
        Task AddBookingAsync(Booking booking, string userId);
        Task RemoveBookingAsync(int remBookingId);
        Task ChangeBookingAsync(Booking booking, int oldBookingId);
    }
}
