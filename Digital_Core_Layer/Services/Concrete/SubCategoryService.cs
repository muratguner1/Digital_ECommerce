using AutoMapper;
using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Core_Layer.Services.Concrete;

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public SubCategoryService(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task<BaseResponseModel> CreateSubCategory(SubCategoryDTO dto)
    {
        var result = await _subCategoryRepository.CreateSubCategory(dto);
        return result;
    }
}