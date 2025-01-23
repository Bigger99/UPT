using System.Text.Json.Serialization;

namespace UPT.Infrastructure.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    None = 0,
    Client,
    Trainer,
    Admin
}

