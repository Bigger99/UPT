using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Chat : HasIdBase
{
    public Trainer Trainer { get; protected set; } = default!;
    public Client Client { get; protected set; } = default!;

    [MaxLength(255)]
    public string Message { get; protected set; } = default!;

    public DateTime Time { get; protected set; } = default!;

    public Chat(Trainer trainer, Client client, string message, DateTime time)
    {
        Trainer = trainer;
        Client = client;
        Message = message;
        Time = time;
    }

    // for EF
    protected Chat()
    {
        
    }
}
