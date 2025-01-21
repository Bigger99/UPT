using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.UserFeatures.Requests;

public class GetByEmailQuery
{
    [Required] public string EmailAddress { get; set; } = default!;
}
