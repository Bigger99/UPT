using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.FavoritFeatures.Requests;

public class AddFavoritCommand
{
    [Required] public int ClientId { get; set; }
    [Required] public int TrainerId { get; set; } = default!;
}
