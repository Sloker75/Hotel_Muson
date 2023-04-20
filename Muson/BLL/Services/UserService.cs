using BLL.Services.Interfaces;
using DLL.Repository;
using Domain.Models;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class UserService : IExtraServiceService
    {
        private readonly ExtraServiceRepository _extraServiceRepository;

        public UserService(ExtraServiceRepository extraServiceRepository)
        {
            _extraServiceRepository = extraServiceRepository;
        }
        public async Task AddExtraServiceAsync(ExtraService extraService, string userId)
            => await _extraServiceRepository.CreateExtraServiceAsync(extraService, userId);

        public async Task ChangeRoomAsync(ExtraService extraService, int oldExtraServiceId)
            => await _extraServiceRepository.ChangeExtraServiceAsync(extraService, oldExtraServiceId);

        public async Task<IReadOnlyCollection<ExtraService>> GetAllAsync() => await _extraServiceRepository.GetAllAsync();

        public async Task RemoveExtraServiceAsync(int remExtraServiceId)
            => await _extraServiceRepository.DeleteExtraServiceAsync(remExtraServiceId);

        public async Task<IReadOnlyCollection<ExtraService>> FindByConditionAsync(Expression<Func<ExtraService, bool>> predicat)
            => await _extraServiceRepository.FindByConditionAsync(predicat);
    }
}
