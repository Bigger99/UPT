using Mapster;
using System.ComponentModel.DataAnnotations;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.PaymentFeatures.Dto;

public class PaymentDto : IDto
{
    public int Id { get; protected set; } = default!;
    public UserDto User { get; protected set; } = default!;
    public DateTime Date { get; protected set; } = default!;

    [MaxLength(255)]
    public string Title { get; protected set; } = default!;

    public decimal Amount { get; protected set; } = default!;

    static PaymentDto()
    {
        TypeAdapterConfig<Payment, PaymentDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>());
    }
}
