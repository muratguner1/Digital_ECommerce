using System.ComponentModel.DataAnnotations.Schema;
using Digital_Domain_Layer.Entities;
using Digital_Domain_Layer.Enums;

namespace Digital_Infrastructure_Layer.DTOs;

public class GetProductDTO
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductDescription { get; set; }
    public int ProductStock { get; set; }
    public string Color { get; set; }
    public virtual ICollection<ProductImage> ProductImages { get; set; }
    public Guid SubCategoryId { get; set; }
}