using Mapster;
using System.ComponentModel.DataAnnotations;
using UPT.Domain.Entities;
using UPT.Features.Features.UserFeatures.Dto;
using UPT.Infrastructure.Interfaces;

namespace UPT.Features.Features.PaymentFeatures.Dto;

public class PaymentDto : IDto
{
    public int Id { get; set; } = default!;
    public UserDto User { get; set; } = default!;
    public DateTime Date { get; set; } = default!;

    [MaxLength(255)]
    public string Title { get; set; } = default!;

    public decimal Amount { get; set; } = default!;

    static PaymentDto()
    {
        TypeAdapterConfig<Payment, PaymentDto>
            .NewConfig()
            .Map(dest => dest.User, src => src.User.Adapt<UserDto>());
    }
}
