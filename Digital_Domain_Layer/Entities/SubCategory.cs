using System.ComponentModel.DataAnnotations.Schema;

namespace Digital_Domain_Layer.Entities;

public class SubCategory
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public virtual ICollection<MainCategory> MainCategories { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}