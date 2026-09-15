using Digital_Core_Layer.Services.Abstract;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.AspNetCore.Http;

namespace Digital_Core_Layer.Services.Concrete;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;

    public ProductImageService(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }


    public async Task<BaseResponseModel> UploadImage(List<IFormFile> files, Guid productId)
    {
        var result = await _productImageRepository.UploadImage(files, productId);
        return result;
    }
}