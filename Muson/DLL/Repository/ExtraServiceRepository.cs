using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class ExtraServiceRepository : BaseRepository<ExtraService>, IExtraServiceRepository
    {
        public ExtraServiceRepository(MusonHotelContext _musonHotelContext) : base(_musonHotelContext)
        {
        }

        public async Task CreateExtraServiceAsync(ExtraService extraService, string userId)
        {
            User user = _musonHotelContext.Users.Find(userId);
            user.ExtraServices.Add(extraService);
            base._musonHotelContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task DeleteExtraServiceAsync(int remServiceId)
        {
            Entities.Remove(Entities.Find(remServiceId));
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task ChangeExtraServiceAsync(ExtraService newExtraService, int oldServiceId)
        {
            var oldExtraService = Entities.Find(oldServiceId);

            oldExtraService.TypeExtraService = newExtraService.TypeExtraService;
            oldExtraService.Price = newExtraService.Price;
            oldExtraService.ExecutionTime = newExtraService.ExecutionTime;
            oldExtraService.CreationTime = newExtraService.CreationTime;
            oldExtraService.RoomNumber = newExtraService.RoomNumber;

            base._musonHotelContext.Entry(oldExtraService).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async override Task<IReadOnlyCollection<ExtraService>> GetAllAsync()
            => await this.Entities.Include(x => x.User).ThenInclude(x => x.Bookings)
            .ThenInclude(x => x.Room).ToListAsync().ConfigureAwait(false);

        public async override Task<IReadOnlyCollection<ExtraService>> FindByConditionAsync(Expression<Func<ExtraService, bool>> predicat)
            => await this.Entities.Include(x => x.User).ThenInclude(x => x.Bookings)
            .ThenInclude(x => x.Room).Where(predicat).ToListAsync().ConfigureAwait(false);

    }
}
