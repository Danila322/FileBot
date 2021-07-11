using FileBot.Data;
using FileBot.Models;
using FileBot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FileBot.Services
{
    public class DirectoryRepository : BaseRepository<Directory>, IDirectoryRepository
    {
        public DirectoryRepository(BotDbContext context) : base(context)
        {
        }

        protected override DbSet<Directory> Set => Context.Directories;
    }
}
