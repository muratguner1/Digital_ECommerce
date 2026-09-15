using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.Model;
using Microsoft.AspNetCore.Http;

namespace Digital_Core_Layer.Services.Abstract;

public interface IProductImageService
{
    Task<BaseResponseModel> UploadImage(List<IFormFile> files, Guid productId);
}