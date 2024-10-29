using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.PaymentFeatures.Requests;

public class DeletePaymentCommand
{
    [Required]
    public int PaymentId { get; set; } = default!;
}
