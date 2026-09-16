using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Persistence_Layer.Repositories.Concrete;

public class MainCategoryRepository : Repository<MainCategory>, IMainCategoryRepository
{
    public MainCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<BaseResponseModel> CreateMainCategory(MainCategoryDTO dto)
    {
        var result = await Add(new MainCategory
        {
            CategoryName = dto.CategoryName,
            CategoryDescription = dto.CategoryDescription,
        });

        if (result != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Main Category Created Successfully",
                Result = result,
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Main Category Not Created",
        };
    }

    public async Task<BaseResponseModel> GetAllMainCategories()
    {
        var result = await GetWithIncludeProperties(x => x.SubCategories);
        if (result != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Main Category List",
                Result = result,
            };
        }

        return new BaseResponseModel
        {
            Success = false,
        };
    }
}