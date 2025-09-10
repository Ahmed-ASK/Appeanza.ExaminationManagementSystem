using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Common
{
    public abstract class DbInitializer(DbContext _dbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any()) 
            {
                await _dbContext.Database.MigrateAsync();
            }
        }

        public abstract Task SeedAsync();
    }
}
