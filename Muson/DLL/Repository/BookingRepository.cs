﻿using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Domain.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace DLL.Repository
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository 
    {
        private readonly IMapper _mapper;
        public BookingRepository(MusonHotelContext _musonHotelContext)
            : base(_musonHotelContext)
        {

        }

        public async Task ChangeBookingAsync(BookingViewModel newBooking, int oldBookingId)
        {
            var oldBooking = Entities.Find(oldBookingId);
            _mapper.Map(newBooking, oldBooking);

            /*oldBooking.DateArrival = newBooking.DateArrival;
            oldBooking.DateDeparture = newBooking.DateDeparture;
            oldBooking.Price = newBooking.Price;
            oldBooking.RoomId = newBooking.Room.Id;
            oldBooking.Room = newBooking.Room;
            oldBooking.ServiceId = newBooking.Service.Id;
            oldBooking.Service = newBooking.Service;*/
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
        {
            return await this.Entities.Include(x => x.Room).Include(x => x.Service)
            .Include(x => x.User).ThenInclude(x => x.ExtraServices).ToListAsync().ConfigureAwait(false);
        }

        public async override Task<IReadOnlyCollection<Booking>> FindByConditionAsync(Expression<Func<Booking, bool>> predicat)
        {
            return await this.Entities.Include(x => x.Room).Include(x => x.Service)
            .Include(x => x.User).ThenInclude(x => x.ExtraServices).Where(predicat)
            .ToListAsync().ConfigureAwait(false);
        }
           
    }
}
