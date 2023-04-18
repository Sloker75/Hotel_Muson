using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(MusonHotelContext _musonHotelContext) : base(_musonHotelContext)
        {
        }

        public async Task ChangeRoomAsync(RoomViewModel newRoom, int oldRoomId)
        {
            var oldRoom = Entities.Find(oldRoomId);

            oldRoom.RoomNumber = newRoom.RoomNumber;
            oldRoom.CountRoom = newRoom.CountRoom;
            oldRoom.Floor = newRoom.Floor;
            oldRoom.TypeRoom = newRoom.TypeRoom;
            oldRoom.Status = newRoom.Status;

            base._musonHotelContext.Entry(oldRoom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(int remRoomId)
        {
            Entities.Remove(Entities.Find(remRoomId));
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async override Task<IReadOnlyCollection<Room>> FindByConditionAsync(Expression<Func<Room, bool>> predicat)
            => await this.Entities.Include(x => x.Bookings).Where(predicat).ToListAsync().ConfigureAwait(false);

        public async override Task<IReadOnlyCollection<Room>> GetAllAsync()
            => await this.Entities.Include(x => x.Bookings).ToListAsync().ConfigureAwait(false);

    }
}
