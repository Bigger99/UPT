using UPT.Features.Features.PaymentFeatures.Dto;

namespace UPT.Features.Services.Payment;

public interface IPaymentService
{
    Task<PaymentDto> Add(int userId, string title, decimal amount);
    Task Delete(int paymentId);
    Task<List<PaymentDto>?> Get(int userId);
}