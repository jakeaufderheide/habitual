using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Habitual.Core.Repositories
{
    public interface Repository<T>
    {
        Task Create(T entity);
        Task Delete(Guid id);
        Task<List<T>> GetAll(string username);
    }
}
