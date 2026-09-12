using Digital_Core_Layer.Services.Abstract;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Interface;

namespace Digital_Core_Layer.Services.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponseModel> Register(RegisterModel model)
    {
        var result = await _userRepository.Register(model);
        return result;
    }
}