using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Common;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence._Data
{
    public class ExaminationDbInitializer(ExaminationDbContext _dbContext) : DbInitializer(_dbContext) , IExaminationDbInitializer
    {
        public override Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
