using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace DLL.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly IMapper _mapper;
        public RoomRepository(MusonHotelContext _musonHotelContext, IMapper mapper) 
            : base(_musonHotelContext)
        {
            _mapper = mapper;
        }

        public async Task ChangeRoomAsync(RoomViewModel newRoom, int oldRoomId)
        {
            var oldRoom = Entities.Find(oldRoomId);
            _mapper.Map<RoomViewModel, Room>(newRoom, oldRoom);
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
