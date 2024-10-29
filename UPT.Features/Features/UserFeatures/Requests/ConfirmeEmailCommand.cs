using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.UserFeatures.Requests;

public class ConfirmeEmailCommand
{
    [Required] public int UserId { get; set; }
}
