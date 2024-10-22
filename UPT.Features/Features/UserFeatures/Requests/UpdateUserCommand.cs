using System.ComponentModel.DataAnnotations;
using UPT.Domain.Entities;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Features.UserFeatures.Requests;

public class UpdateUserCommand
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; } = default!;
    [Required] public string PhoneNumber { get; set; } = default!;
    [Required] public string EmailAddress { get; set; } = default!;
    [Required] public City City { get; set; } = default!;
    [Required] public Gender Gender { get; set; } = default!;
}
