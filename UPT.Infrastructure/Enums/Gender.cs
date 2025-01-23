using System.Text.Json.Serialization;

namespace UPT.Infrastructure.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    None = 0,
    Male,
    Female
}
