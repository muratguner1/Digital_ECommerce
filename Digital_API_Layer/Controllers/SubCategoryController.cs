using Digital_Core_Layer.Services.Abstract;
using Digital_Infrastructure_Layer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubCategoryController : ControllerBase
{
    private readonly ISubCategoryService _subCategoryService;

    public SubCategoryController(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    [HttpPost("CreateSubCategory")]
    public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryDTO dto)
    {
        var response = await _subCategoryService.CreateSubCategory(dto);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}