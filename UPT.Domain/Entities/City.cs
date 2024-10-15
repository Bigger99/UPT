using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class City : HasNameBase
{
    public City(string name)
    {
        Name = name;
    }

    protected City()
    {
        
    }
}
