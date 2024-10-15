using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Favorit : HasIdBase
{
    public Client Client { get; protected set; } = default!;
    public List<Trainer> Trainer { get; protected set; } = default!;

    public Favorit(Client client, List<Trainer> trainer)
    {
        Client = client;
        Trainer = trainer;
    }

    // for EF
    protected Favorit()
    {
        
    }
}
