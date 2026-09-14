using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Interface;

namespace Digital_Core_Layer.Services.Concrete;

public class MainCategoryService : IMainCategoryService
{
    private readonly IMainCategoryRepository _mainCategoryRepository;

    public MainCategoryService(IMainCategoryRepository mainCategoryRepository)
    {
        _mainCategoryRepository = mainCategoryRepository;
    }

    public async Task<BaseResponseModel> CreateMainCategory(MainCategoryDTO dto)
    {
        var result = await _mainCategoryRepository.CreateMainCategory(dto);
        return result;
    }

    public async Task<BaseResponseModel> GetAllMainCategories()
    {
        var result = await _mainCategoryRepository.GetAllMainCategories();
        return result;
    }
}