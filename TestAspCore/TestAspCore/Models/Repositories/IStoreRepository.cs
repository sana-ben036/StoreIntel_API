using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAspCore.Models.Repositories
{
    public interface IStoreRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task Update(TEntity entityChanges);
        Task Delete(Guid id);
    }
}
