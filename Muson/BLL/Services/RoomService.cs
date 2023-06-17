using BLL.Services.Interfaces;
using DLL.Repository;
using Domain.Models;
using Domain.Models.ViewModels;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly RoomRepository _roomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task AddRoomAsync(RoomViewModel roomVM)
        {
            Room room = new Room()
            {
                RoomNumber = roomVM.RoomNumber,
                CountRoom = roomVM.CountRoom,
                Floor = roomVM.Floor,
                Price = roomVM.Price,
                ShortDescription = roomVM.ShortDescription,
                LongDescription = roomVM.LongDescription,
                TypeRoom = roomVM.TypeRoom,
                Status = roomVM.Status,
            };
            await _roomRepository.CreateAsync(room);
        }

        public async Task<IReadOnlyCollection<Room>> GetAllAsync() => await _roomRepository.GetAllAsync();

        public async Task<IReadOnlyCollection<Room>> FindByConditionAsync(Expression<Func<Room, bool>> predicat)
            => await _roomRepository.FindByConditionAsync(predicat);

        public async Task RemoveRoomAsync(int remRoomId) => await _roomRepository.DeleteRoomAsync(remRoomId);

        public async Task ChangeRoomAsync(RoomViewModel roomVM, int oldRoomId)
            => await _roomRepository.ChangeRoomAsync(roomVM, oldRoomId);
    }
}
