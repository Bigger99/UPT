namespace UPT.Infrastructure.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = default!;

    public long Total { get; set; } = default!;

    /// <summary>
    /// For deserialization
    /// </summary>
    public PagedResponse()
    {

    }

    public PagedResponse(IEnumerable<T> items, long total)
    {
        Total = total;
        Items = items;
    }
}
