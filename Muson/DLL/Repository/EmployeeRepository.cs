using DLL.Context;
using DLL.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(MusonHotelContext _musonHotelContext) : base(_musonHotelContext)
        {

        }

        public async override Task<IReadOnlyCollection<Employee>> FindByConditionAsync(Expression<Func<Employee, bool>> predicat)
            => await this.Entities.Where(predicat).ToListAsync().ConfigureAwait(false);

        public async override Task<IReadOnlyCollection<Employee>> GetAllAsync()
            => await this.Entities.ToListAsync().ConfigureAwait(false);
    }
}
