using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileBot.Services.Abstractions
{
    public interface IRepository<T>
    {
        public Task<T> Get(long id);

        public Task<IEnumerable<T>> GetAll();

        public Task Add(T item);

        public Task Remove(T item);

        public Task Save();
    }
}
