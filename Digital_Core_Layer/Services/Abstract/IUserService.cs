using Digital_Persistence_Layer.Model;

namespace Digital_Core_Layer.Services.Abstract;

public interface IUserService
{
    Task<BaseResponseModel> Register(RegisterModel model);
}