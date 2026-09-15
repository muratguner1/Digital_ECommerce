using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.AspNetCore.Http;

namespace Digital_Persistence_Layer.Repositories.Concrete;

public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<BaseResponseModel> UploadImage(List<IFormFile> files, Guid productId)
    {
        if (files == null || files.Count == 0)
        {
            return new BaseResponseModel
            {
                Success = false,
                Message = "No file uploaded."
            };
        }

        var folderPath = Path.Combine("wwwroot", "images");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var uploadImages = new List<ProductImage>();
        foreach (var item in files)
        {
            var filePath = Path.Combine(folderPath, $"{productId}_{item.FileName}");
            await using var stream = new FileStream(filePath, FileMode.Create);
            await item.CopyToAsync(stream);

            uploadImages.Add(new ProductImage
            {
                ProductId = productId,
                ImageUrl = filePath,
            });
        }

        var result = await AddRange(uploadImages);

        if (result != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Images Uploaded Successfully",
                Result = result,
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Images Uploaded Failed",
        };
    }
}