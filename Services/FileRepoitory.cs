using FileBot.Data;
using FileBot.Models;
using FileBot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FileBot.Services
{
    public class FileRepoitory : BaseRepository<File>, IFileRepository
    {
        public FileRepoitory(BotDbContext context) : base(context)
        {
        }

        protected override DbSet<File> Set => Context.Files;
    }
}
