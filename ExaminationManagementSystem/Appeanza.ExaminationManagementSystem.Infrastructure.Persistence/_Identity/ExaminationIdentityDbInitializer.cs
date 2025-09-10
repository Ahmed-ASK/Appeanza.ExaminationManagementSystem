using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Common;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence._Identity
{
    public class ExaminationIdentityDbInitializer(ExaminationIdentityDbContext _dbContext)
        : DbInitializer(_dbContext), IExaminationIdentityDbInitializer
    {
        public override async Task SeedAsync()
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = new List<IdentityRole>()
                {
                    new IdentityRole()
                    {
                        Name = "Student",
                        NormalizedName = "STUDENT",
                    },
                    new IdentityRole()
                    {
                        Name = "Teacher",
                        NormalizedName = "TEACHER",
                    }
                };
                await _dbContext.AddRangeAsync(roles);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
