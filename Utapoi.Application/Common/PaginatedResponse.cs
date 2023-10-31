namespace Utapoi.Application.Common;

public class PaginatedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

    public int Count { get; set; }

    public int TotalCount { get; set; }
}