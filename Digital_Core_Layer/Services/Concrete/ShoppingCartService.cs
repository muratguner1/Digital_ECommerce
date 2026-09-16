using Digital_Core_Layer.Services.Abstract;
using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;

namespace Digital_Core_Layer.Services.Concrete;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
    }

    public async Task<BaseResponseModel> GetCartItems()
    {
        var result = await _shoppingCartRepository.GetCartItems();
        return result;
    }

    public async Task<BaseResponseModel> AddToCart(Guid productId)
    {
        Product? product = await _productRepository.GetById(productId);
        if (product == null)
            return new BaseResponseModel { Success = false, Message = "No product has found" };

        var result = await _shoppingCartRepository.AddToCart(productId, product);
        return result;
    }

    public async Task<BaseResponseModel> RemoveFromCart(Guid productId)
    {
        var result = await _shoppingCartRepository.RemoveFromCart(productId);
        return result;
    }

    public async Task<BaseResponseModel> RemoveCart(Guid cartId)
    {
        var result = await _shoppingCartRepository.RemoveCart(cartId);
        return result;
    }
}