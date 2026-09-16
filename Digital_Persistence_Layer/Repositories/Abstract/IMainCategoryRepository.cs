using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Abstract;

public interface IMainCategoryRepository : IRepository<MainCategory>
{
    Task<BaseResponseModel> CreateMainCategory(MainCategoryDTO dto);
    Task<BaseResponseModel> GetAllMainCategories();
}