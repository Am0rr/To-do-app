using TA.DAL.Enums;

namespace TA.DAL.Models;

public class TaskFilterModel
{
    public string? SearchTerm { get; set; }
    public Guid? CategoryId { get; set; }
    public TaskItemStatus? Status { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}