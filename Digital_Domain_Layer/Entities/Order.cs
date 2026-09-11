using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Domain_Layer.Entities;

public class Order
{
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    
    [ForeignKey(nameof(Checkout))]
    public Guid CheckoutId { get; set; }
    public virtual Checkout Checkout { get; set; }
}