using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Digital_Domain_Layer.Base;

namespace Digital_Domain_Layer.Entities;

public class SubCategory : BaseEntity
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    [ForeignKey(nameof(MainCategoryId))] public Guid MainCategoryId { get; set; }
    [JsonIgnore] public virtual MainCategory MainCategories { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}