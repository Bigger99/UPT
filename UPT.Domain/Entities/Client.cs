using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Client : HasIdBase
{
    public User Person { get; protected set; } = default!;
    public int Height { get; protected set; } = default!;
    public double Weight { get; protected set; } = default!;
    public double VolumeBreast { get; protected set; } = default!;
    public double VolumeWaist { get; protected set; } = default!;
    public double VolumeAbdomen { get; protected set; } = default!;
    public double VolumeButtock { get; protected set; } = default!;
    public double VolumeHip { get; protected set; } = default!;
    public Trainer Trainer { get; protected set; } = default!;

    public bool IsDeleted { get; protected set; } = false;

    public Client(User person, int height, double weight,
    double volumeBreast, double volumeWaist, double volumeAbdomen,
    double volumeButtock, double volumeHip)
    {
        Person = person;
        Height = height;
        Weight = weight;
        VolumeBreast = volumeBreast;
        VolumeWaist = volumeWaist;
        VolumeAbdomen = volumeAbdomen;
        VolumeButtock = volumeButtock;
        VolumeHip = volumeHip;
    }

    public void SetTrainer(Trainer trainer)
    {
        Trainer = trainer;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    // for EF
    protected Client()
    {

    }
}
