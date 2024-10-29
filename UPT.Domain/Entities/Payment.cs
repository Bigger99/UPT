using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Payment : HasIdBase
{
    public User User { get; protected set; } = default!;

    /// <summary>
    /// Дата оплаты
    /// </summary>
    public DateTime Date { get; protected set; } = default!;

    [MaxLength(255)]
    public string Title { get; protected set; } = default!;

    public decimal Amount { get; protected set; } = default!;

    public Payment(User user, DateTime date, string title, decimal amount)
    {
        User = user;
        Date = date;
        Title = title;
        Amount = amount;
    }

    // for EF
    protected Payment()
    {

    }
}
