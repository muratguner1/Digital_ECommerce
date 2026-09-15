using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Infrastructure_Layer.DTOs;

public class SubCategoryDTO
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public Guid MainCategoryId { get; set; }
}