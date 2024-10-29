using UPT.Features.Features.PaymentFeatures.Dto;
using UPT.Infrastructure.Enums;

namespace UPT.Features.Services.Payment;

public interface IPaymentService
{
    Task<PaymentDto> Add(int userId, string title, decimal amount, PurchasedProduct purchasedProduct);
    Task Delete(int paymentId);
    Task<List<PaymentDto>?> Get(int userId);
}