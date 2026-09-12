using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer.AppDbContext;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace Digital_Persistence_Layer.Repositories.Concrete;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : base(context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public Task<BaseResponseModel> Login(LoginModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponseModel> Register(RegisterModel model)
    {
        bool isAnyUser = await isAnyItem(x => x.Email == model.Email);
        if (isAnyUser)
        {
            return new BaseResponseModel
            {
                Success =  false,
                Message = "This email is already registered",
            };
        }
        
        var user = new User
        {
            Email = model.Email,
            UserName = model.Email,
            Name = model.Name,
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var isItTrue = await _roleManager.RoleExistsAsync("Admin");
            if (!isItTrue)
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
                await _userManager.AddToRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "Supervisor");
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            return new BaseResponseModel
            {
                Success = true,
                Message = "User created successfully",
            };
        }
        else
        {
            return new BaseResponseModel
            {
                Success = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }
    }
}