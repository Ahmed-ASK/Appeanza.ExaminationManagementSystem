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
            IEnumerable<IdentityRole> roles = new List<IdentityRole>();
            if (!_dbContext.Roles.Any())
            {
                roles = new List<IdentityRole>()
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
            if (!_dbContext.RoleClaims.Any())
            {
                var teacherRole = _dbContext.Roles.FirstOrDefault(R => R.NormalizedName == "STUDENT");
                var studentRole = _dbContext.Roles.FirstOrDefault(R => R.NormalizedName == "TEACHER");

                var roleClaims = new List<IdentityRoleClaim<string>>()
                {
                    new IdentityRoleClaim<string>
                    {
                        RoleId = teacherRole.Id,
                        ClaimType = "CREATE_EXAM",
                        ClaimValue = "true"
                    },
                    new IdentityRoleClaim<string>
                    {
                        RoleId = teacherRole.Id,
                        ClaimType = "CREATE_GROUP",
                        ClaimValue = "true"
                    },
                    new IdentityRoleClaim<string>
                    {
                        RoleId = studentRole.Id,
                        ClaimType = "JOIN_GROUP",
                        ClaimValue = "true"
                    },
                    new IdentityRoleClaim<string>
                    {
                        RoleId = studentRole.Id,
                        ClaimType = "EXAMINATE",
                        ClaimValue = "true"
                    }
                };

                await _dbContext.RoleClaims.AddRangeAsync(roleClaims);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
