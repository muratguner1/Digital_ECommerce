using Digital_Infrastructure_Layer;
using Digital_Persistence_Layer.MappingProfiles;
using Digital_Persistence_Layer.Model;
using Digital_Persistence_Layer.Repositories.Concrete;
using Digital_Persistence_Layer.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Digital_Persistence_Layer;

public static class ServiceRegistrations
{
    public static void AddPersistenceServiceRegistration(this IServiceCollection Services,
        IConfiguration Configuration = null)
    {
        Services.AddScoped(typeof(BaseResponseModel));
        Services.AddScoped<IUserRepository, UserRepository>();
        Services.AddScoped<IMainCategoryRepository, MainCategoryRepository>();
        Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        Services.AddScoped<IProductRepository, ProductRepository>();
        Services.AddScoped<IProductImageRepository, ProductImageRepository>();
        Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        Services.AddInfrastructureRegisterServices(Configuration);
        Services.AddAutoMapper(typeof(MapperProfile));
    }
}