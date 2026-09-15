using System.ComponentModel.DataAnnotations.Schema;
using Digital_Domain_Layer.Base;

namespace Digital_Domain_Layer.Entities;

public class ProductImage : BaseEntity
{
    public string ImageUrl { get; set; }
    [ForeignKey(nameof(Product))] public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}