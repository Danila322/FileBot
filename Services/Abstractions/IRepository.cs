using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileBot.Services.Abstractions
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Get(long id);

        public Task Add(T item);

        public void Remove(T item);

        public Task Save();
    }
}
