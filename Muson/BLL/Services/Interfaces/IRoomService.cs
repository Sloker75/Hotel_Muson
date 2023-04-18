using Domain.Models;
using Domain.Models.ViewModels;

namespace BLL.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IReadOnlyCollection<Room>> GetAllAsync();
        //Task<IReadOnlyCollection<Booking>> GetAllBookingsAsync();
        Task AddRoomAsync(RoomViewModel roomVM);
        Task RemoveRoomAsync(int remRoomId);
        Task ChangeRoomAsync(RoomViewModel roomVM, int oldRoomId);
    }
}
