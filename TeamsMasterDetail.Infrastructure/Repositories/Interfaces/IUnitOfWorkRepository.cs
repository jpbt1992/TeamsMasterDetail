using Microsoft.EntityFrameworkCore;

namespace TeamsMasterDetail.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWorkRepository<TContext> : IDisposable where TContext : DbContext
    {
        TContext Context { get; }

        Task BeginTransactionAsync();

        Task CommitAsync();

        IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
