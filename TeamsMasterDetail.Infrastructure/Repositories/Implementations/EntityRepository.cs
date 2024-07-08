using System.Linq.Expressions;
using TeamsMasterDetail.Domain.Context;
using TeamsMasterDetail.Infrastructure.Helpers;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Repositories.Implementations
{
    public class EntityRepository<TEntity>(IUnitOfWorkRepository<AppDbContext> unitOfWorkRepository)
        : IEntityRepository<TEntity> where TEntity : class
    {
        #region Private Methods
        private readonly IGenericRepository<TEntity> entityRepository = unitOfWorkRepository.GetRepository<TEntity>();
        #endregion

        #region Commands
        public async Task<int> CommandAsync(TEntity entity, CommandMode commandMode, CancellationToken cancellationToken)
        {
            await unitOfWorkRepository.BeginTransactionAsync();

            switch (commandMode)
            {
                case CommandMode.Create:
                    await entityRepository.AddAsync(entity);
                    break;

                case CommandMode.Update:
                    entityRepository.Update(entity);
                    break;

                case CommandMode.Delete:
                    entityRepository.Remove(entity);
                    break;

                default:
                    throw new NotImplementedException();
            }

            int result = await unitOfWorkRepository.SaveChangesAsync(cancellationToken);
            await unitOfWorkRepository.CommitAsync();

            return result;
        }
        #endregion

        #region Queries
        public Task<List<TEntity>> ToListAsync
            => entityRepository.ToListAsync;

        public ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
            => entityRepository.FindAsync(keyValues);

        public virtual IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
            => entityRepository.Where(predicate);

        public virtual Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => entityRepository.SingleOrDefaultAsync(predicate);
        #endregion
    }
}