using TA.DAL.Entities.Tasks;

namespace TA.DAL.Models;

public class TaskPagedResult
{
    public IEnumerable<TaskItem> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}