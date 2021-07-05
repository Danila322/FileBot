using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileBot.Services.Abstractions
{
    public interface IRepository<T>
    {
        public Task<T> Get(long id);

        public Task Add(T item);

        public Task<bool> Exist(long id);

        public void Remove(T item);

        public Task Save();
    }
}
