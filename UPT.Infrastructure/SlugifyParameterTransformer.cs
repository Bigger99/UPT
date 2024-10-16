using Microsoft.AspNetCore.Routing;
using static System.Text.RegularExpressions.Regex;

namespace UPT.Infrastructure;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        // Slugify value
        return value == null
            ? null
            : Replace(value.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}