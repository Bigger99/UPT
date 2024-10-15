using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Domain.Entities;

public class Person : HasNameBase
{
    public string PhoneNumber { get; protected set; }
    public string EmailAddress { get; protected set; }
    public City City { get; protected set; }
    public Role Role { get; protected set; }
    public Gender Gender { get; protected set; }

    public Person(string phoneNumber, string emailAddress, City city, Role role, Gender gender)
    {
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
