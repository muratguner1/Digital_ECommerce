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
    public static void AddCoreRegisterServices(this IServiceCollection services, IConfiguration configuration = null)
    {
        services.AddPersistenceServiceRegistration(configuration);
        services.AddScoped<IUserService, UserService>();
        services.AddDbContext<ApplicationDbContext>(opt=> opt.UseNpgsql(configuration?.GetConnectionString("DefaultConnection")));
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}