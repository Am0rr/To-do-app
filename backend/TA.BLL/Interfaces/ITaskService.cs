using TA.BLL.DTOs.Tasks;
using TA.DAL.Models;

namespace TA.BLL.Interfaces;

public interface ITaskService
{
    Task<TaskResponse> CreateAsync(Guid userId, CreateTaskRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, Guid userId, string role, UpdateTaskRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, Guid userId, string role, CancellationToken cancellationToken = default);
    Task<TaskResponse> GetByIdAsync(Guid id, Guid userId, string role, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskResponse>> GetAllAsync(Guid userId, string role, CancellationToken cancellationToken = default);
    Task<TaskPagedResponse> GetPagedAsync(Guid userId, string role, TaskFilterModel filter, CancellationToken cancellationToken = default);
}