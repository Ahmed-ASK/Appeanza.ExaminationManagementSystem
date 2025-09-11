
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
            services.AddScoped(typeof(IAuthService))
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            return services;
        }
    }
}
