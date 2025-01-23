using System.Text.Json.Serialization;

namespace UPT.Infrastructure.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TimeOfDay
{
    /// <summary>
    /// С 9 до 12
    /// </summary>
    Morning = 0,

    /// <summary>
    /// С 12 до 18
    /// </summary>
    Day,

    /// <summary>
    /// С 18 до 22
    /// </summary>
    Evening
}
