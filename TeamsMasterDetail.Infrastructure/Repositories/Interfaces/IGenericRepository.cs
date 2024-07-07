using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace TeamsMasterDetail.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region Commands
        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity);

        Task AddRangeAsync(params TEntity[] entities);

        void Update(TEntity entity);

        EntityEntry<TEntity> Remove(TEntity entity);

        void RemoveRange(params TEntity[] entities);
        #endregion

        #region Queries
        Task<List<TEntity>> ToListAsync { get; }

        ValueTask<TEntity?> FindAsync(params object?[]? keyValues);

        IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);

        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
