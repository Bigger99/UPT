using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;
using UPT.Infrastructure.Enums;

namespace UPT.Domain.Entities;

public class User : HasNameBase
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; protected set; } = default!;

    [Required]
    public string PasswordHash { get; protected set; } = default!;

    public string? PhoneNumber { get; protected set; } = default!;
    public City? City { get; protected set; } = default!;
    public Role? Role { get; protected set; } = default!;
    public Gender? Gender { get; protected set; } = default!;

    public User(string email, string passwordHash)
    {
        EmailAddress = email;
        PasswordHash = passwordHash;
    }

    public void AddUserData(string name, string phoneNumber, string emailAddress, City city, Role role, Gender gender)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
        City = city;
        Role = role;
        Gender = gender;
    }

    // for EF
    protected User()
    {

    }
}
