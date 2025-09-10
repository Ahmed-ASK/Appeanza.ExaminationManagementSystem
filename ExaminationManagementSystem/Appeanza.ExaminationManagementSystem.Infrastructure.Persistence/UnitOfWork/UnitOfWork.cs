using Appeanza.ExaminationManagementSystem.Domain.Common;
using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence;
using Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data;
using Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.GenericRepository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork<TEntity, TKey> (ExaminationDbContext _dbContext): IUnitOfWork<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ConcurrentDictionary<string, object> _repository = new();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity, TKey>)_repository.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));
        }
        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    }
}
