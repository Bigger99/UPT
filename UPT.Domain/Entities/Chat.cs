using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Chat : HasIdBase
{
    public User Sender { get; protected set; } = default!;
    public User Recipient { get; protected set; } = default!;

    [MaxLength(255)]
    public string Message { get; protected set; } = default!;

    public DateTime Time { get; protected set; } = default!;

    public Chat(User sender, User recipient, string message, DateTime time)
    {
        Sender = sender;
        Recipient = recipient;
        Message = message;
        Time = time;
    }

    // for EF
    protected Chat()
    {
        
    }
}
