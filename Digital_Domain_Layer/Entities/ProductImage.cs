using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Domain_Layer.Entities;

public class ProductImage
{
    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}