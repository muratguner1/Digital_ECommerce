using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDTO dto)
    {
        var response = await _productService.AddProduct(dto);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var response = await _productService.GetAllProducts();
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("GetProductById/{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var response = await _productService.GetProductById(id);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("DeleteProduct/{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var response = await _productService.RemoveProduct(id);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDTO dto)
    {
        var response = await _productService.UpdateProduct(dto);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}