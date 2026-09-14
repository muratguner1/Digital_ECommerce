using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MainCategoryController : ControllerBase
{
    private readonly IMainCategoryService _mainCategoryService;

    public MainCategoryController(IMainCategoryService mainCategoryService)
    {
        _mainCategoryService = mainCategoryService;
    }

    [HttpPost("CreateMainCategory")]
    public async Task<IActionResult> CreateMainCategory([FromBody] MainCategoryDTO dto)
    {
        var response = await _mainCategoryService.CreateMainCategory(dto);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }

    [HttpGet("GetAllMainCategories")]
    public async Task<IActionResult> GetAllMainCategories()
    {
        var response = await _mainCategoryService.GetAllMainCategories();
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }
}