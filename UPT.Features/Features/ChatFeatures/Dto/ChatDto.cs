using Mapster;
using System.ComponentModel.DataAnnotations;
using UPT.Domain.Entities;
using UPT.Features.Features.PaymentFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.ChatFeatures.Dto;

public class ChatDto : IDto
{
    public int Id { get; set; } = default!;

    public int SenderId { get; set; } = default!;

    public int RecipientId { get; set; } = default!;

    [MaxLength(255)]
    public string Message { get; set; } = default!;

    public DateTime Time { get; set; } = default!;

    static ChatDto()
    {
        TypeAdapterConfig<Payment, PaymentDto>
            .NewConfig();
    }
}
