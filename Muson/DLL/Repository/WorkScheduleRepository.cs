using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class WorkScheduleRepository : BaseRepository<WorkSchedule>, IWorkScheduleRepository
    {
        public WorkScheduleRepository(MusonHotelContext _musonHotelContext) : base(_musonHotelContext)
        {

        }

        public async Task ChangeWorkScheduleAsync(WorkSchedule newWorkSchedule, int oldWorkScheduleId)
        {
            var oldWorkSchedule = Entities.Find(oldWorkScheduleId);

            oldWorkSchedule.DayOfWeek = newWorkSchedule.DayOfWeek;
            oldWorkSchedule.StartTime = newWorkSchedule.StartTime;
            oldWorkSchedule.EndTime = newWorkSchedule.EndTime;
            oldWorkSchedule.EmployeeId = newWorkSchedule.Employee.Id;
            oldWorkSchedule.Employee = newWorkSchedule.Employee;
            

            base._musonHotelContext.Entry(oldWorkSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task CreateWorkScheduleAsync(WorkSchedule workSchedule, int employeeId)
        {
            Employee employee = _musonHotelContext.Employees.Find(employeeId);
            employee.WorkSchedules.Add(workSchedule);
            base._musonHotelContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async Task DeleteWorkScheduleAsync(int remWorkScheduleId)
        {
            Entities.Remove(Entities.Find(remWorkScheduleId));
            await base._musonHotelContext.SaveChangesAsync();
        }

        public async override Task<IReadOnlyCollection<WorkSchedule>> GetAllAsync()
           => await this.Entities.Include(x => x.Employee).ThenInclude(x => x.User).ToListAsync().ConfigureAwait(false);

        public async override Task<IReadOnlyCollection<WorkSchedule>> FindByConditionAsync(Expression<Func<WorkSchedule, bool>> predicat)
            => await this.Entities.Include(x => x.Employee).ThenInclude(x => x.User).Where(predicat).ToListAsync().ConfigureAwait(false);

    }
}
