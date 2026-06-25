using TA.BLL.DTOs.Tasks;
using TA.DAL.Models;

namespace TA.BLL.Interfaces;

public interface ITaskService
{
    Task<TaskResponse> CreateAsync(Guid userId, CreateTaskRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Guid userId, UpdateTaskRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<TaskResponse> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskResponse>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<TaskPagedResponse> GetPagedAsync(Guid userId, TaskFilterModel filter, CancellationToken cancellationToken = default);
}