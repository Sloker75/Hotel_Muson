using Domain.Models;
using Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository.Interfaces
{
    public interface IRoomRepository
    {
        Task ChangeRoomAsync(RoomViewModel newRoom, int oldRoomId);
        Task DeleteRoomAsync(int remRoomId);
    }
}
