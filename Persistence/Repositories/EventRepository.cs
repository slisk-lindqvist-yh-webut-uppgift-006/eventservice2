using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Helper;
using Persistence.Interfaces;
using Persistence.Models;

namespace Persistence.Repositories;

public interface IEventRepository : IBaseRepository<EventEntity, string>
{
    // IQueryable<EventEntity> GetAllQuery();
}

public class EventRepository(DataContext context)
    : BaseRepository<EventEntity, string>(context), IEventRepository
{
    // public IQueryable<EventEntity> GetAllQuery()
    // {
    //     return _dbSet
    //         .Include(x => x.Packages)
    //         .Include(x => x.Location);
    // }

    public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync()
    {
        try
        {
            // var result = await GetAllQuery().ToListAsync();
            var result = await _dbSet
                .Include(x => x.Packages)
                .ToListAsync();
            
            return RepositoryResultFactory.Success<IEnumerable<EventEntity>>(result);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<IEnumerable<EventEntity>>(500, ex.Message);
        }
    }

    public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync(Expression<Func<EventEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbSet
                .Include(x => x.Packages)
                .Where(predicate)
                .ToListAsync();
            
            // var result = await _dbSet
            //     .Include(x => x.Packages)
            //     .Include(x => x.Location)
            //     .Where(predicate)
            //     .ToListAsync();

            return RepositoryResultFactory.Success<IEnumerable<EventEntity>>(result);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<IEnumerable<EventEntity>>(500, ex.Message);
        }
    }

    public override async Task<RepositoryResult<EventEntity?>> GetAsync(string id)
    {
        try
        {
            var result = await _dbSet
                .Include(x => x.Packages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                return RepositoryResultFactory.Error<EventEntity?>(404, "Event not found.");

            return RepositoryResultFactory.Success<EventEntity?>(result, 200, "Event retrieved successfully.");
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<EventEntity?>(500, ex.Message);
        }
    }

    public override async Task<RepositoryResult<EventEntity?>> GetAsync(Expression<Func<EventEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbSet
                .AsNoTracking()
                .Include(x => x.Packages)
                .FirstOrDefaultAsync(predicate);

            if (result == null)
                return RepositoryResultFactory.Error<EventEntity?>(404, "Event not found.");

            return RepositoryResultFactory.Success<EventEntity?>(result, 200, "Event retrieved successfully.");
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<EventEntity?>(500, ex.Message);
        }
    }
}
