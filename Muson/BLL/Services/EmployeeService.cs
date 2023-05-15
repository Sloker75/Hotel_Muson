using BLL.Services.Interfaces;
using DLL.Repository;
using DLL.Repository.Interfaces;
using Domain.Models;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class EmployeeService : IWorkScheduleService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly WorkScheduleRepository _workScheduleRepository;

        public EmployeeService(EmployeeRepository employeeRepository, WorkScheduleRepository workScheduleRepository)
        {
            _employeeRepository = employeeRepository;
            _workScheduleRepository = workScheduleRepository;
        }

        #region WorkSchedule
        public async Task AddWorkScheduleAsync(WorkSchedule WorkSchedule, int employeeId)
            => await _workScheduleRepository.CreateWorkScheduleAsync(WorkSchedule, employeeId);

        public async Task ChangeWorkScheduleAsync(WorkSchedule WorkSchedule, int oldWorkScheduleId)
            => await _workScheduleRepository.ChangeWorkScheduleAsync(WorkSchedule, oldWorkScheduleId);

        public async Task<IReadOnlyCollection<WorkSchedule>> GetAllWorkScheduleAsync()
            => await _workScheduleRepository.GetAllAsync();

        public async Task RemoveWorkScheduleAsync(int remWorkScheduleId)
            => await _workScheduleRepository.DeleteWorkScheduleAsync(remWorkScheduleId);

        public async Task<IReadOnlyCollection<WorkSchedule>> FindByConditionWorkScheduleAsync(Expression<Func<WorkSchedule, bool>> predicat)
            => await _workScheduleRepository.FindByConditionAsync(predicat);
        #endregion

        #region Employee
        public async Task<IReadOnlyCollection<Employee>> GetAllEmployeeAsync()
            => await _employeeRepository.GetAllAsync();

        public async Task<IReadOnlyCollection<Employee>> FindByConditionEmployeeAsync(Expression<Func<Employee, bool>> predicat)
            => await _employeeRepository.FindByConditionAsync(predicat);
        #endregion
    }
}
