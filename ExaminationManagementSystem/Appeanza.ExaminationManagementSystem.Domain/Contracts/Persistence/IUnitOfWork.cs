using Appeanza.ExaminationManagementSystem.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity,TKey> GetRepositroy<TEntity,TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();
    }
}
