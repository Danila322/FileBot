using FileBot.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FileBot.Services.Abstractions
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected BotDbContext Context { get; }

        protected abstract DbSet<T> Set { get; }

        protected BaseRepository(BotDbContext context)
        {
            this.Context = context;
        }

        public async virtual Task<T> Get(long id)
        {
            return await Set.FindAsync(id);
        }

        public async virtual Task Add(T item)
        {
            await Set.AddAsync(item);
        }
        
        public virtual void Remove(T item)
        {
            Set.Remove(item);
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}
