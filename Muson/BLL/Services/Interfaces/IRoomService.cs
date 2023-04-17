using Domain.Models;


namespace BLL.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IReadOnlyCollection<Room>> GetAllAsync();
        //Task<IReadOnlyCollection<Booking>> GetAllBookingsAsync();
        Task AddRoomAsync(Room room);
        Task RemoveRoomAsync(int remRoomId);
    }
}
