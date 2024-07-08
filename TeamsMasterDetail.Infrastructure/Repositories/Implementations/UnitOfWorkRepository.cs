using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using TeamsMasterDetail.Infrastructure.Repositories.Interfaces;

namespace TeamsMasterDetail.Infrastructure.Repositories.Implementations
{
    public class UnitOfWorkRepository<TContext>(TContext context) : IUnitOfWorkRepository<TContext> where TContext : DbContext
    {
        #region Private Variables
        private bool disposed = false;

        private IDbContextTransaction? transaction;

        private readonly Dictionary<Type, object> repositories = [];
        #endregion

        #region Public Methods
        public TContext Context => context;

        public async Task BeginTransactionAsync()
        {
            transaction ??= await context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            try
            {
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (repositories.TryGetValue(typeof(TEntity), out object? repository))
            {
                return (IGenericRepository<TEntity>)repository;
            }

            GenericRepository<TEntity, TContext> newRepository = new(context);

            repositories.TryAdd(typeof(TEntity), newRepository);

            return newRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<object> entities = context.ChangeTracker.Entries()
                          .Where(where => where.State == EntityState.Added
                              || where.State == EntityState.Deleted
                              || where.State == EntityState.Modified)
                          .Select(select => select.Entity);

            if (entities.Any())
            {
                await Task.Run(() =>
                {
                    entities.AsParallel().ForAll(entity =>
                    {
                        ValidationContext validationContext = new(entity);
                        Validator.ValidateObject(entity, validationContext, true);
                    });

                }, cancellationToken);
            }

            return await context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }
    }
}
