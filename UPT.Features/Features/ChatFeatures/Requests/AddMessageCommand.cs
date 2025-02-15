using System.ComponentModel.DataAnnotations;

namespace UPT.Features.Features.ChatFeatures.Requests;

public class AddChatCommand
{
    /// <summary>
    /// Пользователь отправивший сообщение
    /// </summary>
    [Required]
    public int SenderId { get; set; } = default!;

    /// <summary>
    /// Пользователь получающий сообщение
    /// </summary>
    [Required]
    public int RecipientId { get; set; } = default!;

    /// <summary>
    /// Оплаченный продукт
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Message { get; set; } = default!;
}
