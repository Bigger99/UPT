using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class Notification : HasNameBase
{
    public DateTime CreationDate { get; protected set; } = default!;

    [MaxLength(255)]
    public string Text { get; protected set; } = default!;

    public User User { get; protected set; } = default!;
    public bool IsChecked { get; protected set; } = false;

    public Notification(string name, DateTime creationDate, string text, User user)
    {
        Name = name;
        CreationDate = creationDate;
        Text = text;
        User = user;
    }

    public void EditNews(string name, DateTime creationDate, string text, User user)
    {
        Name = name;
        CreationDate = creationDate;
        Text = text;
        User = user;
    }

    public void SetChecked()
    {
        IsChecked = true;
    }

    // for EF
    protected Notification()
    {
        
    }
}
