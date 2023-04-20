using Domain.Models;

namespace BLL.Services.Interfaces
{
    public interface IExtraServiceService
    {
        Task<IReadOnlyCollection<ExtraService>> GetAllAsync();
        Task AddExtraServiceAsync(ExtraService extraService, string userId);
        Task RemoveExtraServiceAsync(int remExtraServiceId);
        Task ChangeRoomAsync(ExtraService extraService, int oldExtraServiceId);
    }
}
