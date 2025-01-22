using Refit;
using UPT.Features.Features.PaymentFeatures.Dto;
using UPT.Features.Features.PaymentFeatures.Requests;

namespace UPT.Tests.API.Frontend.UserTests.Base;

internal interface IPaymentProvider
{
    [Get("/api/web/payment/get")]
    Task<ApiResponse<List<PaymentDto>?>> Get([Query] int userId);  
    
    [Delete("/api/web/payment/delete")]
    Task<IApiResponse> Delete([Query] int paymentId);

    [Post("/api/web/payment/add")]
    Task<ApiResponse<PaymentDto>> Add([Body] AddPaymentCommand command);
}