using Digital_Core_Layer.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpPost("AddToCart")]
    public async Task<IActionResult> AddToCart(Guid productId)
    {
        var response = await _shoppingCartService.AddToCart(productId);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("GetCart")]
    public async Task<IActionResult> GetCart()
    {
        var response = await _shoppingCartService.GetCartItems();
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("RemoveFromCart")]
    public async Task<IActionResult> RemoveFromCart(Guid productId)
    {
        var response = await _shoppingCartService.RemoveFromCart(productId);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("RemoveCart")]
    public async Task<IActionResult> RemoveCart(Guid cartId)
    {
        var response = await _shoppingCartService.RemoveCart(cartId);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}