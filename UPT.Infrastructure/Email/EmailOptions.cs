namespace UPT.Infrastructure.Email;

public class EmailOptions
{
    public required string FromEmail { get; init; }
    public required string Host { get; init; }
    public required int Post { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
}
