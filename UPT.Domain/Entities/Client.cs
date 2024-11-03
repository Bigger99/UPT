using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Client : HasIdBase
{
    public User User { get; protected set; } = default!;
    public int Height { get; protected set; } = default!;
    public double Weight { get; protected set; } = default!;
    public double VolumeBreast { get; protected set; } = default!;
    public double VolumeWaist { get; protected set; } = default!;
    public double VolumeAbdomen { get; protected set; } = default!;
    public double VolumeButtock { get; protected set; } = default!;
    public double VolumeHip { get; protected set; } = default!;
    public Trainer Trainer { get; protected set; } = default!;

    public bool IsDeleted { get; protected set; } = false;

    public Client(User user, int height, double weight,
    double volumeBreast, double volumeWaist, double volumeAbdomen,
    double volumeButtock, double volumeHip)
    {
        User = user;
        Height = height;
        Weight = weight;
        VolumeBreast = volumeBreast;
        VolumeWaist = volumeWaist;
        VolumeAbdomen = volumeAbdomen;
        VolumeButtock = volumeButtock;
        VolumeHip = volumeHip;
    }
    
    public void Update(int height, double weight,
        double volumeBreast, double volumeWaist, double volumeAbdomen,
        double volumeButtock, double volumeHip)
    {
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
