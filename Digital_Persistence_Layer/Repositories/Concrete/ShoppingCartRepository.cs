using System.Security.Claims;
using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Abstract;
using Microsoft.AspNetCore.Http;

namespace Digital_Persistence_Layer.Repositories.Concrete;

public class ShoppingCartRepository : Repository<Cart>, IShoppingCartRepository
{
    private readonly ApplicationDbContext _context;
    private readonly string _UserId;

    public ShoppingCartRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) :
        base(context)
    {
        _context = context;
        _UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }

    public async Task<BaseResponseModel> GetCartItems()
    {
        var cart = await GetWhere(x => x.UserId == _UserId, x => x.Products);
        if (cart != null)
        {
            return new BaseResponseModel
            {
                Success = true,
                Message = "Cart fetched items successfully",
                Result = cart
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Cart not fetched successfully",
        };
    }

    public async Task<BaseResponseModel> AddToCart(Guid productId, Product? product)
    {
        var cart = await GetWhere(x => x.UserId == _UserId, x => x.Products);
        //var product1 = await GetByIdGeneric<Product>(productId);
        if (product == null)
            return new BaseResponseModel { Success = false, Message = "Product not found" };

        if (cart == null)
        {
            var newCart = new Cart
            {
                UserId = _UserId,
                Products = new List<Product>()
            };

            newCart.Products.Add(product);
            var result = await Add(newCart);

            if (result != null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = "Product added to cart successfully",
                    Result = result
                };
            }
        }
        else
        {
            cart.Products.Add(product);
            var result = await Update(cart, cart.Id);

            if (result != null)
            {
                return new BaseResponseModel
                {
                    Success = true,
                    Message = "Product added to cart successfully",
                    Result = result
                };
            }
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Product not added to cart successfully",
        };
    }

    public async Task<BaseResponseModel> RemoveFromCart(Guid productId)
    {
        var cart = await GetWhere(x => x.UserId == _UserId, x => x.Products);
        if (cart != null)
        {
            var product = cart.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                cart.Products.Remove(product);
                var result = await Update(cart, cart.Id);
                if (result != null)
                {
                    return new BaseResponseModel
                    {
                        Success = true,
                        Message = "Product removed from cart successfully",
                        Result = result
                    };
                }

                return new BaseResponseModel
                {
                    Success = false,
                    Message = "Product not removed from cart successfully",
                };
            }

            return new BaseResponseModel
            {
                Success = false,
                Message = "Product not find",
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Cart not found",
        };
    }

    public async Task<BaseResponseModel> RemoveCart(Guid cartId)
    {
        var cart = await GetWhere(x => x.Id == cartId, x => x.Products);
        if (cart != null)
        {
            cart.Products.Clear();
            await Delete(cartId);
            return new BaseResponseModel
            {
                Success = true,
                Message = "Cart removed successfully",
                Result = cart,
            };
        }

        return new BaseResponseModel
        {
            Success = false,
            Message = "Cart not found",
        };
    }
}