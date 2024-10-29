using Mapster;
using Microsoft.EntityFrameworkCore;
using UPT.Data;
using UPT.Features.Features.PaymentFeatures.Dto;
using UPT.Infrastructure.Middlewars;

namespace UPT.Features.Services.Payment;

public class PaymentService(UPTDbContext dbContext) : IPaymentService
{
    public async Task<List<PaymentDto>?> Get(int userId)
    {
        var payments = await dbContext.Payments
            .Include(x => x.User)
                .ThenInclude(x => x.City)
            .Where(x => x.User.Id == userId)
            .ToListAsync();

        if (payments is null)
        {
            return null;
        }

        return payments.Select(x => x.Adapt<PaymentDto>()).ToList();
    }

    public async Task<PaymentDto> Add(int userId, string title, decimal amount)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new BackendException("User not found");

        var newPayment = new Domain.Entities.Payment(user, DateTime.UtcNow, title, amount);
        await dbContext.Payments.AddAsync(newPayment);
        await dbContext.SaveChangesAsync();

        return newPayment.Adapt<PaymentDto>();
    }

    public async Task Delete(int paymentId)
    {
        var payment = await dbContext.Payments
            .FirstOrDefaultAsync(x => x.Id == paymentId) ?? throw new BackendException("Payment not found"); ;

        dbContext.Payments.Remove(payment);
        await dbContext.SaveChangesAsync();
    }
}
