using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository.Interfaces
{
    public interface IRoomRepository
    {
        Task ChangeRoomAsync(Room newRoom, int oldRoomId);
        Task DeleteRoomAsync(int remRoomId);
    }
}
