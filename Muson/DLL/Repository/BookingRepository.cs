using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(MusonHotelContext _musonHotelContext) : base(_musonHotelContext)
        {

        }

        public async Task ChangeBookingAsync(Booking newBooking, int oldBookingId)
        {
            var oldBooking = Entities.Find(oldBookingId);

            oldBooking.DateArrival = newBooking.DateArrival;
            oldBooking.DateDeparture = newBooking.DateDeparture;
            oldBooking.Price = newBooking.Price;
            oldBooking.RoomId = newBooking.RoomId;
            oldBooking.Room = newBooking.Room;
            oldBooking.ServiceId = newBooking.ServiceId;
            oldBooking.Service = newBooking.Service;

            base._musonHotelContext.Entry(oldBooking).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task CreateBookingAsync(Booking booking, string userId)
        {
            User user = _musonHotelContext.Users.Find(userId);
            user.Bookings.Add(booking);
            base._musonHotelContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int remBookingId)
        {
            Entities.Remove(Entities.Find(remBookingId));
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async override Task<IReadOnlyCollection<Booking>> GetAllAsync()
            => await this.Entities.Include(x => x.Room).Include(x => x.Service)
            .Include(x => x.User).ThenInclude(x => x.ExtraServices).ToListAsync().ConfigureAwait(false);

        public async override Task<IReadOnlyCollection<Booking>> FindByConditionAsync(Expression<Func<Booking, bool>> predicat)
            => await this.Entities.Include(x => x.Room).Include(x => x.Service)
            .Include(x => x.User).ThenInclude(x => x.ExtraServices).Where(predicat).ToListAsync().ConfigureAwait(false);
    }
}
