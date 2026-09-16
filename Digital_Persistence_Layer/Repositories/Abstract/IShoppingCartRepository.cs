using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.Model;

namespace Digital_Persistence_Layer.Repositories.Abstract;

public interface IShoppingCartRepository : IRepository<Cart>
{
    Task<BaseResponseModel> GetCartItems();
    Task<BaseResponseModel> AddToCart(Guid productId, Product? product);
    Task<BaseResponseModel> RemoveFromCart(Guid productId);
    Task<BaseResponseModel> RemoveCart(Guid cartId);
}