using Digital_Domain_Layer.Base;

namespace Digital_Domain_Layer.Entities;

public class MainCategory : BaseEntity
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public virtual ICollection<SubCategory> SubCategories { get; set; }
}