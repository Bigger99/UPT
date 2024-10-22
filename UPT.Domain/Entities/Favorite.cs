using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Favorite : HasIdBase
{
    public Client Client { get; protected set; } = default!;
    public Trainer Trainer { get; protected set; } = default!;

    public Favorite(Client client, Trainer trainer)
    {
        Client = client;
        Trainer = trainer;
    }

    // for EF
    protected Favorite()
    {
        
    }
}
