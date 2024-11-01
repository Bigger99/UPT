using System.ComponentModel.DataAnnotations;
using UPT.Domain.Base;

namespace UPT.Domain.Entities;

public class News : HasNameBase
{
    public DateTime CreationDate { get; protected set; } = default!;

    public DateTime? EditDate { get; protected set; } = default!;

    [MaxLength(255)]
    public string Text { get; protected set; } = default!;

    public User User { get; protected set; } = default!;

    public byte[]? Image { get; protected set; } = default!;

    public News(string name, DateTime creationDate, string text, User user, byte[]? image)
    {
        Name = name;
        CreationDate = creationDate;
        Text = text;
        User = user;
        Image = image;
    }

    public void UpdateNews(string name, DateTime editDate, string text, User user, byte[]? image)
    {
        EditDate = editDate;
        Name = name;
        Text = text;
        User = user;
        Image = image;
    }

    // for EF
    protected News()
    {
        
    }
}
