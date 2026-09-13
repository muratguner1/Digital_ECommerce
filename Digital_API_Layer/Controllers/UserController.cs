using Digital_Core_Layer.Services.Abstract;
using Digital_Persistence_Layer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Digital_API_Layer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
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
    
    [HttpPost("login")]
    public async Task<IActionResult> SignIn([FromBody]LoginModel model)
    {
        var response = await _userService.Login(model);
        if (response != null)
        {
            return  Ok(response);
        }
        return BadRequest();
    }

}