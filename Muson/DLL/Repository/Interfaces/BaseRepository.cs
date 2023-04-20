using DLL.Context;
using DLL.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace DLL.Repository.Interfaces
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected MusonHotelContext _musonHotelContext;

        public BaseRepository(MusonHotelContext musonHotelContext)
        {
            _musonHotelContext = musonHotelContext;
        }

        private DbSet<TEntity> _entities;
        protected DbSet<TEntity> Entities => this._entities ??= _musonHotelContext.Set<TEntity>();

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
            => await this.Entities.ToListAsync().ConfigureAwait(false);

        public virtual async Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicat)
            => await this.Entities.Where(predicat).ToListAsync().ConfigureAwait(false);

        public virtual async Task<OperationDetail> CreateAsync(TEntity entity)
        {
            try
            {
                await this.Entities.AddAsync(entity).ConfigureAwait(false);
                await this._musonHotelContext.SaveChangesAsync();
                return new OperationDetail() { Text = "Created", IsCompleted = true };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Not Created");
                return new OperationDetail() { Text = "Not Created", IsCompleted = false };
            }
        }
    }
}
