using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Feedback : HasIdBase
{
    public DateTime Date { get; protected set; } = default!;
    public double Rating { get; protected set; } = default!;

    [MaxLength(255)]
    public string Text { get; protected set; } = default!;

    public Client Creator { get; protected set; } = default!;
    public Trainer Trainer { get; protected set; } = default!;

    public Feedback(DateTime date, double rating, string text, Client creator, Trainer trainer)
    {
        Date = date;
        Rating = rating;
        Text = text;
        Creator = creator;
        Trainer = trainer;
    }

    // for EF
    protected Feedback()
    {

    }
}
