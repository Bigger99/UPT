using System.Text.Json.Serialization;

namespace UPT.Infrastructure.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Deadline
{
    Mounth3 = 0,
    Mounth6,
    Mounth12
}
