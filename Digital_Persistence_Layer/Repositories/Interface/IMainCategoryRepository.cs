using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Interface;

public interface IMainCategoryRepository
{
    Task<BaseResponseModel> CreateMainCategory(MainCategoryDTO dto);
    Task<BaseResponseModel> GetAllMainCategories();
}