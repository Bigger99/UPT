using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Domain.Entities;

public class Person : HasNameBase
{
    public string PhoneNumber { get; protected set; } = default!;
    public string EmailAddress { get; protected set; } = default!;
    public City City { get; protected set; } = default!;
    public Role Role { get; protected set; } = default!;
    public Gender Gender { get; protected set; } = default!;

    public Person(string name, string phoneNumber, string emailAddress, City city, Role role, Gender gender)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
        City = city;
        Role = role;
        Gender = gender;
    }

    // for EF
    protected Person()
    {

    }
}
