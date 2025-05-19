using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Helper;
using Persistence.Interfaces;
using Persistence.Models;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity, TKey>(DbContext context) : IBaseRepository<TEntity, TKey> where TEntity : class
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction? _transaction;

    #region Create/Add

        public virtual async Task<RepositoryResult<bool>> AddAsync(TEntity entity)
        {
            if (entity == null)
                return RepositoryResultFactory.Error<bool>(400, "Entity cannot be null");

            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return RepositoryResultFactory.Success(true, 201, "Entity added successfully.");
            }
            catch (Exception ex)
            {
                return RepositoryResultFactory.Error<bool>(500, ex.Message);
            }
        }

    #endregion

    #region Read/Get

        #region Get All - No Exclusions

            public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
            {
                try
                {
                    var result = await _dbSet.ToListAsync();
                    return RepositoryResultFactory.Success<IEnumerable<TEntity>>(result);
                }
                catch (Exception ex)
                {
                    return RepositoryResultFactory.Error<IEnumerable<TEntity>>(500, ex.Message);
                }
            }

        #endregion

        #region Get All - With Exclusions (For search into names, places, dates etc.)

            public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
            {
                try
                {
                    var result = await _dbSet.Where(predicate).ToListAsync();
                    return RepositoryResultFactory.Success<IEnumerable<TEntity>>(result);
                }
                catch (Exception ex)
                {
                    return RepositoryResultFactory.Error<IEnumerable<TEntity>>(500, ex.Message);
                }
            }

        #endregion

        #region Get One - Based on Id (Apparently a quicker search alternative)

            public virtual async Task<RepositoryResult<TEntity?>> GetAsync(TKey id)
            {
                try
                {
                    var result = await _dbSet.FindAsync(id);
                    if (result == null)
                        return RepositoryResultFactory.Error<TEntity?>(404, "Entity not found.");
                
                    return RepositoryResultFactory.Success(result, 200, "Entity retrieved successfully.")!;
                }
                catch (Exception ex)
                {
                    return RepositoryResultFactory.Error<TEntity?>(500, ex.Message);
                }
            }

        #endregion

        #region Get One - Based on Name/Date/Location/etc.

            public virtual async Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> predicate)
            {
                try
                {
                    var result = await _dbSet.FirstOrDefaultAsync(predicate);
                    if (result == null)
                        return RepositoryResultFactory.Error<TEntity?>(404, "Entity not found.");

                    return RepositoryResultFactory.Success<TEntity?>(result, 200, "Entity retrieved successfully.");
                }
                catch (Exception ex)
                {
                    return RepositoryResultFactory.Error<TEntity?>(500, ex.Message);
                }
            }

        #endregion

    #endregion

    #region Update

        public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                return RepositoryResultFactory.Error<bool>(400, "Entity cannot be null");
        
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return RepositoryResultFactory.Success(true, 200, "Entity updated successfully.");
            }
            catch (Exception ex)
            {
                return RepositoryResultFactory.Error<bool>(500, ex.Message);
            }
        }

    #endregion

    #region Delete/Remove

        public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                return RepositoryResultFactory.Error<bool>(400, "Entity cannot be null");
        
            try
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return RepositoryResultFactory.Success(true, 200, "Entity deleted successfully.");
            }
            catch (Exception ex)
            {
                return RepositoryResultFactory.Error<bool>(500, ex.Message);
            }
        }

    #endregion

    #region Find/Exists
        
        public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
        {
            var exists = await _dbSet.AnyAsync(findBy);
            return exists
                ? RepositoryResultFactory.Error<bool>(404, "Entity not found.")
                : RepositoryResultFactory.Success(true, 200, "Entity exists.");
        }

    #endregion

    #region Save (Not in use.)

        public virtual async Task<RepositoryResult<bool>> SaveChangesAsync()
        {
            try
            {
                var changes = await _context.SaveChangesAsync();
                return new RepositoryResult<bool>
                {
                    Succeeded = changes > 0,
                    StatusCode = changes > 0 ? 200 : 204,
                    Result = changes > 0,
                    Message = changes > 0 ? "Changes saved successfully." : "No changes were made."
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new RepositoryResult<bool>
                {
                    Succeeded = false,
                    StatusCode = 500,
                    Error = ex.Message
                };
            }
        }

    #endregion

    #region Transaction Management

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

    #endregion
}
