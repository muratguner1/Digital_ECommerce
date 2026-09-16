using AutoMapper;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Persistence_Layer.Repositories.Concrete;

public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
{
    private readonly IMapper _mapper;

    public SubCategoryRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<BaseResponseModel> CreateSubCategory(SubCategoryDTO dto)
    {
        var objMap = _mapper.Map<SubCategory>(dto);
        var result = await Add(objMap);

        if (result != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Sub Category Created Successfully",
                Result = result,
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Sub Category Not Created",
        };
    }
}