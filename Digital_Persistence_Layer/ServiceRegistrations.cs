using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Concrete;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Digital_Persistence_Layer;

public static class ServiceRegistrations
{
    public static void AddPersistenceServiceRegistration(this IServiceCollection services, IConfiguration configuration = null)
    {
        services.AddScoped(typeof(BaseResponseModel));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}