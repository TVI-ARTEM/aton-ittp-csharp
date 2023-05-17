using Microsoft.Extensions.DependencyInjection;
using Users.Bll.Services;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBllInfrastructure(
        this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddTransient<IUserService, UserService>();
        return services;
    }
}