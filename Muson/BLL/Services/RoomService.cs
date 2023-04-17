using BLL.Services.Interfaces;
using DLL.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly RoomRepository _roomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task AddRoomAsync(Room room) => await _roomRepository.CreateAsync(room);

        public async Task<IReadOnlyCollection<Room>> GetAllAsync() => await _roomRepository.GetAllAsync();

        /*public Task<IReadOnlyCollection<Booking>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }*/

        public async Task<IReadOnlyCollection<Room>> FindByConditionAsync(Expression<Func<Room, bool>> predicat)
            => await _roomRepository.FindByConditionAsync(predicat);

        public async Task RemoveRoomAsync(int remRoomId) => await _roomRepository.DeleteRoomAsync(remRoomId);
    }
}
