using FileBot.MarkupBuilders;
using FileBot.Models;

namespace FileBot.Services.Abstractions
{
    public interface IMarkupBuilderFactory
    {
        public IMarkupBuilder<Directory> CreateDirectoriesMarkupBuilder();

        public IMarkupBuilder<Directory> CreateFilesMarkupBuilder();
    }
}
