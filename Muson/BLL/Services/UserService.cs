using BLL.Services.Interfaces;
using DLL.Repository;
using Domain.Models;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class UserService : IExtraServiceService, IBookingService
    {
        private readonly ExtraServiceRepository _extraServiceRepository;
        private readonly BookingRepository _bookingRepository;

        public UserService(ExtraServiceRepository extraServiceRepository, BookingRepository bookingRepository)
        {
            _extraServiceRepository = extraServiceRepository;
            _bookingRepository = bookingRepository;
        }

        #region User

        #endregion


        #region ExtraService
        public async Task AddExtraServiceAsync(ExtraService extraService, string userId)
            => await _extraServiceRepository.CreateExtraServiceAsync(extraService, userId);

        public async Task ChangeRoomAsync(ExtraService extraService, int oldExtraServiceId)
            => await _extraServiceRepository.ChangeExtraServiceAsync(extraService, oldExtraServiceId);

        public async Task<IReadOnlyCollection<ExtraService>> GetAllExtraServiceAsync() 
            => await _extraServiceRepository.GetAllAsync();

        public async Task RemoveExtraServiceAsync(int remExtraServiceId)
            => await _extraServiceRepository.DeleteExtraServiceAsync(remExtraServiceId);

        public async Task<IReadOnlyCollection<ExtraService>> FindByConditionAsync(Expression<Func<ExtraService, bool>> predicat)
            => await _extraServiceRepository.FindByConditionAsync(predicat);


        #endregion

        #region Booking
        public async Task<IReadOnlyCollection<Booking>> GetAllBookingAsync()
            => await _bookingRepository.GetAllAsync();

        public async Task AddBookingAsync(Booking booking, string userId)
            => await _bookingRepository.CreateBookingAsync(booking, userId);

        public async Task RemoveBookingAsync(int remBookingId)
            => await _bookingRepository.DeleteBookingAsync(remBookingId);

        public async Task ChangeBookingAsync(Booking booking, int oldBookingId)
            => await _bookingRepository.ChangeBookingAsync(booking, oldBookingId);

        public async Task<IReadOnlyCollection<Booking>> FindByConditionAsync(Expression<Func<Booking, bool>> predicat)
            => await _bookingRepository.FindByConditionAsync(predicat);

        #endregion

    }
}
