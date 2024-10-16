namespace UPT.Domain.Base;

public class HasNameBase : HasIdBase
{
    public string? Name { get; protected set; } = default!;
}
