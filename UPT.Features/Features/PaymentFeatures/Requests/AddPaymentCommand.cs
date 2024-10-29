using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.PaymentFeatures.Requests;

public class AddPaymentCommand
{
    /// <summary>
    /// Пользователь оплативший продукт
    /// </summary>
    [Required]
    public int UserId { get; set; } = default!;

    /// <summary>
    /// Оплаченный продукт
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = default!;

    /// <summary>
    /// Сумма оплаты
    /// </summary>
    [Required]
    public decimal Amount { get; set; } = default!;
}
