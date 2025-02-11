﻿using System.ComponentModel.DataAnnotations;
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

    public Gender? Gender { get; protected set; } = default!;

    public bool IsNotificationEnable { get; protected set; } = true;
    public bool IsEmailNotificationEnable { get; protected set; } = false;
    public bool IsEmailConfirmed { get; protected set; } = false;
    public string? Avatar { get; protected set; } = default!;
    public bool IsDeleted { get; protected set; } = false;

    public User(string email, string passwordHash)
    {
        EmailAddress = email;
        PasswordHash = passwordHash;
    }

    public void EditUserData(string name, string phoneNumber, string emailAddress, City city, Gender gender, 
        bool isNotificationEnable, bool isEmailNotificationEnable, string? avatar)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
        City = city;
        Gender = gender;
        IsNotificationEnable = isNotificationEnable;
        IsEmailNotificationEnable = isEmailNotificationEnable;
        Avatar = avatar;
    }

    public void ConfirmeEmail()
    {
        IsEmailConfirmed = true;
    }

    public void EditPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void Delete()
    {
        IsDeleted = true;
    }

    // for EF
    protected User()
    {

    }
}
