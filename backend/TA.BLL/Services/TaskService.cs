using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TA.BLL.DTOs.Tasks;
using TA.BLL.Exceptions;
using TA.BLL.Interfaces;
using TA.DAL.Entities.Identity;
using TA.DAL.Entities.Tasks;
using TA.DAL.Enums;
using TA.DAL.Interfaces;
using TA.DAL.Models;

namespace TA.BLL.Services;

public class TaskService : BaseService, ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskResponse> CreateAsync(Guid userId, CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        Validate(request);

        var status = Enum.Parse<TaskItemStatus>(request.Status);

        var task = new TaskItem(request.Title, request.Description, request.CategoryId, userId, status);

        _unitOfWork.Tasks.Add(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TaskResponse>(task);
    }

    public async Task UpdateAsync(Guid id, Guid userId, string role, UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        Validate(request);

        var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Task with Id {id} not found.");

        if (userId != task.UserId && !HasGlobalAccess(role))
            throw new ForbiddenException("You are not allowed to access this task.");

        if (request.Title != null)
            task.ChangeTitle(request.Title);

        if (request.Description != null)
            task.ChangeDescription(request.Description);

        if (request.CategoryId.HasValue)
            task.ChangeCategory(request.CategoryId);

        if (request.Status != null)
        {
            var status = Enum.Parse<TaskItemStatus>(request.Status);

            task.ChangeStatus(status);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, Guid userId, string role, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Task with Id {id} not found.");

        if (userId != task.UserId && !HasGlobalAccess(role))
            throw new ForbiddenException("You are not allowed to access this task.");

        _unitOfWork.Tasks.Delete(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<TaskResponse> GetByIdAsync(Guid id, Guid userId, string role, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Task with ID {id} was not found.");

        if (userId != task.UserId && !HasGlobalAccess(role))
            throw new ForbiddenException("You are not allowed to access this task.");

        return _mapper.Map<TaskResponse>(task);
    }

    public async Task<IEnumerable<TaskResponse>> GetAllAsync(Guid userId, string role, CancellationToken cancellationToken)
    {
        IEnumerable<TaskItem> tasks;

        if (HasGlobalAccess(role))
            tasks = await _unitOfWork.Tasks.GetAllAsync(cancellationToken);
        else
            tasks = await _unitOfWork.Tasks.Query()
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
    }

    public async Task<TaskPagedResponse> GetPagedAsync(Guid userId, string role, TaskFilterModel filter, CancellationToken cancellationToken = default)
    {
        TaskPagedResult result;

        if (HasGlobalAccess(role))
            result = await _unitOfWork.Tasks.GetFilteredPagedAsync(null, filter, cancellationToken);
        else
            result = await _unitOfWork.Tasks.GetFilteredPagedAsync(userId, filter, cancellationToken);

        return _mapper.Map<TaskPagedResponse>(result);
    }

    private static bool HasGlobalAccess(string role) =>
        string.Equals(role, nameof(UserRole.Administrator), StringComparison.OrdinalIgnoreCase);
}