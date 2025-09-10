using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence;
using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence._Data;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence._Identity;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Identity;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configurations)
        {

            services.AddDbContext<ExaminationIdentityDbContext>(options => 
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configurations.GetConnectionString("IdentityContext"));
            });
            services.AddScoped(typeof(IExaminationIdentityDbInitializer), typeof(ExaminationIdentityDbInitializer));

            services.AddDbContext<ExaminationDbContext>(options => 
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configurations.GetConnectionString("ExaminationDbContext"));
            });
            services.AddScoped(typeof(IExaminationDbInitializer), typeof(ExaminationDbInitializer));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            return services;
        
        }



    }
}
