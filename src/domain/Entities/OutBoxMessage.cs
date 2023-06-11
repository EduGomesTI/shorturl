namespace Domain.Entities;

public sealed class OutBoxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime OccurredOn { get; set; }

    public bool Success { get; set; } = false;

    public DateTime? ProcessedOn { get; set; }

    public string? Error { get; set; }
}