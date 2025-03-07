﻿using System.ComponentModel.DataAnnotations;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.UserFeatures.Requests;

public class UpdateUserCommand
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;
    [Required] public string PhoneNumber { get; set; } = default!;
    [Required] public string EmailAddress { get; set; } = default!;
    [Required] public int CityId { get; set; } = default!;
    [Required] public Gender Gender { get; set; } = default!;
    [Required] public bool IsNotificationEnable { get; set; }
    [Required] public bool IsEmailNotificationEnable { get; set; }
    public string? Avatar { get; set; } = default!;
}
