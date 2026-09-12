using System.ComponentModel.DataAnnotations.Schema;
using Digital_Domain_Layer.Base;

namespace Digital_Domain_Layer.Entities;

public class Order : BaseEntity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public virtual User User { get; set; }
    
    [ForeignKey(nameof(Checkout))]
    public Guid CheckoutId { get; set; }
    public virtual Checkout Checkout { get; set; }
}