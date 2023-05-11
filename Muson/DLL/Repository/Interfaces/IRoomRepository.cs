using Domain.Models.ViewModels;

namespace DLL.Repository.Interfaces
{
    public interface IRoomRepository
    {
        Task ChangeRoomAsync(RoomViewModel newRoom, int oldRoomId);
        Task DeleteRoomAsync(int remRoomId);
    }
}
