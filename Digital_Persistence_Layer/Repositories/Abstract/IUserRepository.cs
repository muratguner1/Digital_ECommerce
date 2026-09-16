using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Abstract;

public interface IUserRepository : IRepository<User>
{
    Task<BaseResponseModel> Login(LoginModel model);
    Task<BaseResponseModel> Register(RegisterModel model);
}