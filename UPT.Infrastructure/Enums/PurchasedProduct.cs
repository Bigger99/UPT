using System.Text.Json.Serialization;

namespace UPT.Infrastructure.Enums;

/// <summary>
/// Продаваемые продукты
/// </summary>

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PurchasedProduct
{
    FreeSubscribe = 0,
    ProSubscribe,
    DeluxeSubscribe
}
