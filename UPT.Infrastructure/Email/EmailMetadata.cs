namespace UPT.Infrastructure.Email;

public class EmailMetadata
{
    public string ToAddress { get; private set; }
    public string Subject { get; private set; }
    public string? Body { get; private set; }

    public EmailMetadata(string toAddress, string subject, string? body = "")
    {
        ToAddress = toAddress;
        Subject = subject;
        Body = body;
    }
}