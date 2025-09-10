using Appeanza.ExaminationManagementSystem.Domain.Emtities.Identity;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Identity.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Identity
{
    public class ExaminationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {

        public ExaminationIdentityDbContext(DbContextOptions<ExaminationIdentityDbContext> options)
            :base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfigurations());

        }
    }
}
