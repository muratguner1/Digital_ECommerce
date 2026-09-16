using Microsoft.AspNetCore.Identity;

namespace Digital_Domain_Layer.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }
    public virtual Cart Cart { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
}