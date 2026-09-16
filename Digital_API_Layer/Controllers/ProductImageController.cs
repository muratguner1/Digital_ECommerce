using Digital_Core_Layer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImageController : ControllerBase
{
    private readonly IProductImageService _productImageService;

    public ProductImageController(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    [HttpPost("UploadImage")]
    public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> files, Guid productId)
    {
        var response = await _productImageService.UploadImage(files, productId);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }
}