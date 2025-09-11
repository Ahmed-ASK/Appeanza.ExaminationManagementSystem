using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence;
using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence._Data;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence._Identity;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices
            (
            this IServiceCollection services,
            IConfiguration configurations,
            string identityConnectionStringSection,
            string examinationSystemConnectionStringSectionName
            )
        {

            services.AddDbContext<ExaminationIdentityDbContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configurations.GetConnectionString(identityConnectionStringSection));
            });
            services.AddScoped(typeof(IExaminationIdentityDbInitializer), typeof(ExaminationIdentityDbInitializer));

            services.AddDbContext<ExaminationDbContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(configurations.GetConnectionString(examinationSystemConnectionStringSectionName));
            });
            services.AddScoped(typeof(IExaminationDbInitializer), typeof(ExaminationDbInitializer));
        
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            return services;
        
        }



    }
}
