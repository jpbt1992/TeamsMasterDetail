using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Repositories.Implementations
{
    public class GenericRepository<TEntity, TContext>(TContext context) : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        #region Private Variables
        private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();
        #endregion

        #region Commands
        public virtual ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity)
            => dbSet.AddAsync(entity);

        public virtual Task AddRangeAsync(params TEntity[] entities)
            => dbSet.AddRangeAsync(entities);

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);

            if (context.ChangeTracker.HasChanges())
            {
                context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual EntityEntry<TEntity> Remove(TEntity entity)
            => dbSet.Remove(entity);

        public virtual void RemoveRange(params TEntity[] entities)
            => dbSet.RemoveRange(entities);
        #endregion

        #region Queries
        public virtual Task<List<TEntity>> ToListAsync
            => dbSet.ToListAsync();

        public virtual ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
            => dbSet.FindAsync(keyValues);

        public virtual IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
            => dbSet.Where(predicate);

        public virtual Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => dbSet.SingleOrDefaultAsync(predicate);
        #endregion
    }
}
