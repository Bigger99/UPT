using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Payment : HasIdBase
{
    public User Person { get; protected set; } = default!;
    public DateTime Date { get; protected set; } = default!;

    [MaxLength(255)]
    public string Title { get; protected set; } = default!;

    public decimal Amount { get; protected set; } = default!;

    public Payment(User person, DateTime date, string title, decimal amount)
    {
        Person = person;
        Date = date;
        Title = title;
        Amount = amount;
    }

    // for EF
    protected Payment()
    {

    }
}
