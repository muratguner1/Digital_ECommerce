using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.Model;
using Microsoft.AspNetCore.Http;

namespace Digital_Persistence_Layer.Repositories.Abstract;

public interface IProductImageRepository : IRepository<ProductImage>
{
    Task<BaseResponseModel> UploadImage(List<IFormFile> files, Guid productId);
}