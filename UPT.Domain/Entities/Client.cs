using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

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

    /// <summary>
    /// Программа тренировки выбранная клинтом
    /// </summary>
    public TrainingProgram TrainingProgram { get; protected set; } = default!;

    public bool IsDeleted { get; protected set; } = false;

    public Client(User user, TrainingProgram trainingProgram, int height, double weight,
    double volumeBreast, double volumeWaist, double volumeAbdomen,
    double volumeButtock, double volumeHip)
    {
        User = user;
        TrainingProgram = trainingProgram;
        Height = height;
        Weight = weight;
        VolumeBreast = volumeBreast;
        VolumeWaist = volumeWaist;
        VolumeAbdomen = volumeAbdomen;
        VolumeButtock = volumeButtock;
        VolumeHip = volumeHip;
    }
    
    public void Update(TrainingProgram trainingProgram, int height, double weight,
        double volumeBreast, double volumeWaist, double volumeAbdomen,
        double volumeButtock, double volumeHip)
    {
        TrainingProgram = trainingProgram;
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
