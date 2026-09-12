using System.ComponentModel.DataAnnotations.Schema;
using Digital_Domain_Layer.Base;

namespace Digital_Domain_Layer.Entities;

public class SubCategory : BaseEntity
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public virtual ICollection<MainCategory> MainCategories { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}