using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreMap.Backend.Data.Interfaces
{
    public interface ICrudRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> Insert(T store);
        
        Task<T> Update(T store);

        Task<T> FindOne(Guid id);

        Task<bool> Delete(Guid id);
    }
}