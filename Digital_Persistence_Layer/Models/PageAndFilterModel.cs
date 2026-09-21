namespace Digital_Persistence_Layer.Model;

public class PageAndFilterModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Keyword { get; set; }
}