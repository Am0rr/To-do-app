namespace TA.BLL.DTOs.Tasks;

public record TaskResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid? CategoryId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string Status { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}