using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Interface;

public interface IUserRepository
{
    Task<BaseResponseModel> Login(LoginModel model);
    Task<BaseResponseModel> Register(RegisterModel model); 
}
