using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Gym : HasNameBase
{
    public TimeOnly OpenTime { get; protected set; } = default!;
    public TimeOnly CloseTime { get; protected set; } = default!;
    public string Location { get; protected set; } = default!;

    public List<Trainer> Trainers { get; protected set; } = default!;

    public Gym(string name, TimeOnly openTime, TimeOnly closeTime, string location)
    {
        Name = name;
        OpenTime = openTime;
        CloseTime = closeTime;
        Location = location;
    }

    protected Gym()
    {

    }
}
