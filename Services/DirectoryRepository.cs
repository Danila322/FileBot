using FileBot.Data;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Threading.Tasks;

namespace FileBot.Services
{
    public class DirectoryRepository : IRepository<Directory>
    {
        private readonly BotDbContext context;

        public DirectoryRepository(BotDbContext context)
        {
            this.context = context;
        }

        public async Task Add(Directory item)
        {
            await context.Directories.AddAsync(item);
        }

        public async Task<bool> Exist(long id)
        {
            Directory directory = await context.Directories.FindAsync(id);
            return directory is not null;
        }

        public async Task<Directory> Get(long id)
        {
            return await context.Directories.FindAsync(id);
        }

        public void Remove(Directory item)
        {
            context.Directories.Remove(item);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
