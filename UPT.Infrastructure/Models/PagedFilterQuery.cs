namespace UPT.Infrastructure.Models;

public class PagedFilterQuery<T>
{
    public T Request { get; set; } = default!;

    public string? Search { get; set; } = default!;

    public bool Asc { get; set; } = true;
}
