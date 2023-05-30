using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Users.Bll.Services;
using Users.Bll.Services.Interfaces;
using Users.Bll.Settings;

namespace Users.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBllInfrastructure(
        this IServiceCollection services,
        IConfigurationRoot configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}