using System.Linq.Expressions;
using Persistence.Models;

namespace Persistence.Interfaces;

public interface IBaseRepository<TEntity, TKey>
    where TEntity : class
{
    Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();
    Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<RepositoryResult<TEntity?>> GetAsync(TKey id);
    Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    Task<RepositoryResult<bool>> AddAsync(TEntity entity);
    Task<RepositoryResult<bool>> UpdateAsync(TEntity entity);
    Task<RepositoryResult<bool>> DeleteAsync(TEntity entity);

    Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy);

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}