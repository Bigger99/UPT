using System.ComponentModel.DataAnnotations;

namespace UPT.Domain.Base;

public class HasIdBase
{
    [Key]
    [Required]
    public int Id { get; protected set; }
}
