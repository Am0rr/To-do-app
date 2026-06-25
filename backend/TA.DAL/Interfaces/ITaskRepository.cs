using TA.DAL.Entities.Tasks;
using TA.DAL.Models;

namespace TA.DAL.Interfaces;

public interface ITaskRepository : IBaseRepository<TaskItem>
{
    Task<TaskPagedResult> GetFilteredPagedAsync(Guid? userId, TaskFilterModel filter, CancellationToken cancellationToken = default);
}