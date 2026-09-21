using AutoMapper;
using Digital_Domain_Layer.Entities;
using Digital_Domain_Layer.Extensions;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Persistence_Layer.Repositories.Concrete;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly IMapper _mapper;

    public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<BaseResponseModel> GetAllProducts()
    {
        IEnumerable<Product> products = await GetAll();
        var objMap = _mapper.Map<IEnumerable<GetProductDTO>>(products);
        if (objMap != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "All products fetched successfully",
                Result = objMap
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "No products found",
        };
    }

    public async Task<BaseResponseModel> GetProductById(Guid id)
    {
        Product product = await GetWhere(x => x.Id == id, x => x.ProductImages);
        var objMap = _mapper.Map<GetProductDTO>(product);
        if (objMap != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Product fetched successfully",
                Result = objMap
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Product not found",
        };
    }

    public async Task<BaseResponseModel> AddProduct(ProductDTO dto)
    {
        var action = dto.Color;
        var actionDescription = action.GetDescription();

        var objMap = _mapper.Map<Product>(dto);
        objMap.Color = actionDescription;
        Product product = await Add(objMap);

        if (product != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Product created successfully",
                Result = product
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Product not created",
        };
    }

    public async Task<BaseResponseModel> UpdateProduct(UpdateProductDTO dto)
    {
        var objMap = _mapper.Map<Product>(dto);
        var result = await Update(objMap, dto.Id);

        if (result != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Product updated successfully",
                Result = result
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Product not updated",
        };
    }

    public async Task<BaseResponseModel> RemoveProduct(Guid id)
    {
        await Delete(id);

        return new BaseResponseModel
        {
            Success = true,
            Message = "Product removed successfully",
        };
    }

    public async Task<BaseResponseModel> GetProductsWithPagination(PageAndFilterModel model)
    {
        PagedResult<Product> products = await GetPagedResult(x => (x.ProductName.Contains(model.Keyword)
                                                                   || x.ProductDescription.Contains(model.Keyword)
                                                                   || x.Color.Contains(model.Keyword)
                                                                   || x.SubCategory.CategoryName
                                                                       .Contains(model.Keyword)),
            x => x.OrderByDescending(x => x.ProductPrice), model.PageNumber, model.PageSize, x => x.ProductImages);

        if (products.Items != null)
        {
            return new BaseResponseModel
                { Exception = null, Message = "Products fetched successfully", Result = products };
        }

        return new BaseResponseModel { Exception = null, Message = "Products not found" };
    }
}