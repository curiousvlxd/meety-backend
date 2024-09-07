namespace Domain.Primitives;

public sealed record Pagination
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
