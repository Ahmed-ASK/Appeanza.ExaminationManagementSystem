using Appeanza.ExaminationManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Domain.Contracts.Persistence.DbInitializers
{
    public interface IUnitOfWork <TEntity , TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<int> CompleteAsync();
    }
}
