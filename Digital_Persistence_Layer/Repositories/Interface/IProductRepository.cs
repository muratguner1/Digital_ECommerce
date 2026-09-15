using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Interface;

public interface IProductRepository
{
    Task<BaseResponseModel> GetAllProducts();
    Task<BaseResponseModel> GetProductById(Guid id);
    Task<BaseResponseModel> AddProduct(ProductDTO dto);
    Task<BaseResponseModel> UpdateProduct(UpdateProductDTO dto);
    Task<BaseResponseModel> RemoveProduct(Guid id);
}