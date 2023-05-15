using Domain.Models;
using Domain.Models.ViewModels;

namespace BLL.Services.Interfaces
{
    public interface IWorkScheduleService
    {
        Task<IReadOnlyCollection<WorkSchedule>> GetAllWorkScheduleAsync();
        Task AddWorkScheduleAsync(WorkSchedule WorkSchedule, int employeeId);
        Task RemoveWorkScheduleAsync(int remWorkScheduleId);
        Task ChangeWorkScheduleAsync(WorkSchedule WorkSchedule, int oldWorkScheduleId);
    }
}
