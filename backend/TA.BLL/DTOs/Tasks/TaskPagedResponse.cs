namespace TA.BLL.DTOs.Tasks;

public record TaskPagedResponse(
    IEnumerable<TaskResponse> Items,
    int TotalCount,
    int PageSize,
    int PageNumber,
    int TotalPages
);