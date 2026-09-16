using System.ComponentModel.DataAnnotations.Schema;
using Digital_Domain_Layer.Base;
using Digital_Domain_Layer.Enums;

namespace Digital_Domain_Layer.Entities;

public class Product : BaseEntity
{
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductDescription { get; set; }
    public int ProductStock { get; set; }
    public string Color { get; set; }
    public virtual ICollection<ProductImage> ProductImages { get; set; }

    [ForeignKey(nameof(SubCategory))] public Guid SubCategoryId { get; set; }
    public virtual SubCategory SubCategory { get; set; }
}