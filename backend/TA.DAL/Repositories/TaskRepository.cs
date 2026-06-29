using Microsoft.EntityFrameworkCore;
using TA.DAL.Entities.Tasks;
using TA.DAL.Interfaces;
using TA.DAL.Models;
using TA.DAL.Persistence;

namespace TA.DAL.Repositories;

public class TaskRepository(AppDbContext context)
    : BaseRepository<TaskItem>(context), ITaskRepository
{
    public async Task<TaskPagedResult> GetFilteredPagedAsync(Guid? userId, TaskFilterModel filter, CancellationToken cancellationToken = default)
    {
        IQueryable<TaskItem> query = _dbSet.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            query = query.Where(t => t.Title.Contains(filter.SearchTerm) ||
                                     t.Description != null && t.Description.Contains(filter.SearchTerm));

        if (filter.CategoryId.HasValue)
            query = query.Where(t => t.CategoryId == filter.CategoryId.Value);

        if (filter.Status.HasValue)
            query = query.Where(t => t.Status == filter.Status.Value);

        if (userId.HasValue)
            query = query.Where(t => t.UserId == userId.Value);

        int totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        return new TaskPagedResult
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }
}
