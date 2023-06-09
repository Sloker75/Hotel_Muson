﻿using BLL.Services.Interfaces;
using DLL.Repository;
using Domain.Models;
using Domain.Models.ViewModels;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class UserService : IExtraServiceService, IBookingService, IUserService, ICommentService
    {
        private readonly ExtraServiceRepository _extraServiceRepository;
        private readonly BookingRepository _bookingRepository;
        private readonly UserRepository _userRepository;
        private readonly CommentRepository _commentRepository;

        public UserService(UserRepository userRepository, ExtraServiceRepository extraServiceRepository,
            BookingRepository bookingRepository, CommentRepository commentRepository)
        {
            _extraServiceRepository = extraServiceRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        #region User
        public async Task<IReadOnlyCollection<User>> GetAllUserAsync()
            => await _userRepository.GetAllAsync();
        public async Task ChangeUserAsync(UserViewModel newUser, string oldUserEmail)
            => await _userRepository.ChangeUserAsync(newUser, oldUserEmail);

        public async Task ChangeUserPasswordAsync(User user, string currentPassword, string newPassword)
            => await _userRepository.ChangeUserPasswordAsync(user, currentPassword, newPassword);

        public async Task<bool> RegistrationAsync(UserRegistrationViewModel userRegistrationVM)
            => await _userRepository.RegistrationAsync(userRegistrationVM);

        public async Task DeleteUserAsync(string remUserId)
            => await _userRepository.DeleteUserAsync(remUserId);

        public async Task<IReadOnlyCollection<User>> FindByConditionUserAsync(Expression<Func<User, bool>> predicat)
            => await _userRepository.FindByConditionAsync(predicat);
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

        public async Task<IReadOnlyCollection<ExtraService>> FindByConditionExtraServiceAsync(Expression<Func<ExtraService, bool>> predicat)
            => await _extraServiceRepository.FindByConditionAsync(predicat);


        #endregion

        #region Booking
        public async Task<IReadOnlyCollection<Booking>> GetAllBookingAsync()
            => await _bookingRepository.GetAllAsync();

        public async Task AddBookingAsync(BookingViewModel bookingViewModel, string userId)
        {
            var booking = new Booking
            {
                Id = bookingViewModel.BookingId,
                DateArrival = bookingViewModel.DateArrival,
                DateDeparture = bookingViewModel.DateDeparture,
                Price = bookingViewModel.Room.Price,
                RoomId = bookingViewModel.Room.Id,
                Room = bookingViewModel.Room,
                User = (await FindByConditionUserAsync(x => x.Id == userId)).FirstOrDefault(),
                UserId = userId
            };
            await _bookingRepository.CreateBookingAsync(booking, userId);
        }

        public async Task RemoveBookingAsync(int remBookingId)
            => await _bookingRepository.DeleteBookingAsync(remBookingId);

        public async Task ChangeBookingAsync(BookingViewModel booking, int oldBookingId)
            => await _bookingRepository.ChangeBookingAsync(booking, oldBookingId);

        public async Task<IReadOnlyCollection<Booking>> FindByConditionBookingAsync(Expression<Func<Booking, bool>> predicat)
            => await _bookingRepository.FindByConditionAsync(predicat);

        #endregion

        #region Comment

        public async Task<IReadOnlyCollection<Comment>> GetAllCommentsAsync()
            => await _commentRepository.GetAllAsync();

        public async Task AddCommentAsync(Comment comment, string userId)
            => await _commentRepository.CreateCommentAsync(comment, userId);

        public async Task AddCommentAnswerAsync(Comment comment, int mainCommentId, string userId)
            => await _commentRepository.CreateAnswerAsync(comment, mainCommentId, userId);

        public async Task RemoveCommentAsync(int remCommentId)
            => await _commentRepository.DeleteCommentAsync(remCommentId);

        public async Task ChangeCommentAsync(Comment newComment, int oldCommentId)
            => await _commentRepository.ChangeCommentAsync(newComment, oldCommentId);


        #endregion

    }
}
