using Digital_Core_Layer.Services.Abstract;
using Digital_Persistence_Layer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IUserService _userService;
    public RegisterController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> SignUp([FromBody]RegisterModel model)
    {
        var response = await _userService.Register(model);
        if (response != null)
        {
            return  Ok(response);
        }
        return BadRequest();
    }

}