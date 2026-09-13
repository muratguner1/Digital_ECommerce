using Digital_Core_Layer.Services.Abstract;
using Digital_Core_Layer.Services.Concrete;
using Digital_Domain_Layer.Entities;
using Digital_Persistence_Layer;
using Digital_Persistence_Layer.AppDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Digital_Core_Layer;

public static class ServiceRegistrations
{
    public static void AddCoreRegisterServices(this IServiceCollection Services, IConfiguration Configuration = null)
    {
        Services.AddPersistenceServiceRegistration(Configuration);
        Services.AddScoped<IUserService, UserService>();
        Services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(Configuration?.GetConnectionString("DefaultConnection")));
        Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}