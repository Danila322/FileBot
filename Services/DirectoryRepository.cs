using FileBot.Data;
using FileBot.Models;
using FileBot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FileBot.Services
{
    public class DirectoryRepository : BaseRepository<Directory>, IDirectoryRepository
    {
        private IFileRepository fileRepository;

        public DirectoryRepository(BotDbContext context, IFileRepository fileRepository) : base(context)
        {
            this.fileRepository = fileRepository;
        }

        protected override DbSet<Directory> Set => Context.Directories;
    }
}
