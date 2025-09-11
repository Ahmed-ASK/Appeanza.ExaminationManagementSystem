
using Appeanza.ExaminationManagementSystem.Application.Abstraction.Services;
using Appeanza.ExaminationManagementSystem.Application.Abstraction.Services.Auth;
using Appeanza.ExaminationManagementSystem.Application.Services;
using Appeanza.ExaminationManagementSystem.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Appeanza.ExaminationManagementSystem.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<Func<IAuthService>>(provider => () => provider.GetRequiredService<IAuthService>());
            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
