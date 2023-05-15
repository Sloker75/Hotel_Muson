using Domain.Models;


namespace DLL.Repository.Interfaces
{
    public interface IWorkScheduleRepository
    {
        Task CreateWorkScheduleAsync(WorkSchedule workSchedule, int employeeId);
        Task ChangeWorkScheduleAsync(WorkSchedule newWorkSchedule, int oldWorkScheduleId);
        Task DeleteWorkScheduleAsync(int remWorkScheduleId);
    }
}
