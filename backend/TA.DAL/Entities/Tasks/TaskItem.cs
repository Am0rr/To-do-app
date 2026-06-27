using TA.DAL.Enums;

namespace TA.DAL.Entities.Tasks;

public class TaskItem : BaseEntity
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public TaskItemStatus Status { get; private set; }

    protected TaskItem() { }

    public TaskItem(string title, string? description, Guid? categoryId, Guid userId, TaskItemStatus status = TaskItemStatus.Todo)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;
        UserId = userId;
        Status = status;
    }

    public void ChangeTitle(string newTitle)
    {
        Title = newTitle;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeDescription(string? newDescription)
    {
        Description = newDescription;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeCategory(Guid? newCategoryId)
    {
        CategoryId = newCategoryId;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeStatus(TaskItemStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}