using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Favorite : HasIdBase
{
    public Client Client { get; protected set; } = default!;
    public List<Trainer> Trainers { get; protected set; } = default!;

    public Favorite(Client client, List<Trainer> trainers)
    {
        Client = client;
        Trainers = trainers;
    }

    // for EF
    protected Favorite()
    {
        
    }
}
