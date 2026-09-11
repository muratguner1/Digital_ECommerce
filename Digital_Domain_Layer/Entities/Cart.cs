using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Domain_Layer.Entities;

public class Cart
{
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}