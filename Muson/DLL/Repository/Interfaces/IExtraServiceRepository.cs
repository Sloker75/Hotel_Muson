using Domain.Models;

namespace DLL.Repository.Interfaces
{
    public interface IExtraServiceRepository
    {
        Task CreateExtraServiceAsync(ExtraService extraService, string userId);
        Task ChangeExtraServiceAsync(ExtraService newExtraService, int oldServiceId);
        Task DeleteExtraServiceAsync(int remServiceId);
    }
}
