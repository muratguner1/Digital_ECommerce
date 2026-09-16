using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Abstract;

public interface ISubCategoryRepository : IRepository<SubCategory>
{
    Task<BaseResponseModel> CreateSubCategory(SubCategoryDTO dto);
}