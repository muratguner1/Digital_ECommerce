using Digital_Domain_Layer.Enums;

namespace Digital_Infrastructure_Layer.DTOs;

public class UpdateProductDTO
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductDescription { get; set; }
    public int ProductStock { get; set; }
    public Colors Color { get; set; }
    public Guid SubCategoryId { get; set; }
}