using Digital_Core_Layer.Services.Abstract;
using Digital_Domain_Layer.Entities;
using Digital_Infrastructure_Layer.DTOs;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Core_Layer.Services.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<BaseResponseModel> GetAllProducts()
    {
        var result = await _productRepository.GetAllProducts();
        return result;
    }

    public async Task<BaseResponseModel> GetProductById(Guid id)
    {
        var result = await _productRepository.GetProductById(id);
        return result;
    }

    public async Task<BaseResponseModel> AddProduct(ProductDTO dto)
    {
        var result = await _productRepository.AddProduct(dto);
        return result;
    }

    public async Task<BaseResponseModel> UpdateProduct(UpdateProductDTO dto)
    {
        var result = await _productRepository.UpdateProduct(dto);
        return result;
    }

    public async Task<BaseResponseModel> RemoveProduct(Guid id)
    {
        var result = await _productRepository.RemoveProduct(id);
        return result;
    }
}