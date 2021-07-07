using FileBot.Commands;
using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace FileBot.Services
{
    public class MarkupBuilderFactory : IMarkupBuilderFactory
    {
        public IMarkupBuilder<Directory> CreateDirectoriesMarkupBuilder()
        {
            return new DirectoriesMarkupBuilder();
        }

        public IMarkupBuilder<Directory> CreateFilesMarkupBuilder()
        {
            return new FilesMarkupBuilder();
        }
    }
}
