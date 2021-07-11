using FileBot.Commands;
using FileBot.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Services
{
    public class CommandFactory : ICommandFactory
    {
        private static NullCommand nullCommand = new NullCommand();

        private IEnumerable<ITelegramCommand> commands;

        public CommandFactory(IUserInfoRepository userRepository,
                              IDirectoryRepository directoryRepository,
                              IFileRepository fileRepository,
                              IMarkupBuilderFactory markupBuilderFactory)
        {
            var directoriesMarkupBuilder = markupBuilderFactory.CreateDirectoriesMarkupBuilder();
            var filesMarkupBuilder = markupBuilderFactory.CreateFilesMarkupBuilder();

            commands = new ITelegramCommand[]
            {
                new StartCommand(userRepository),
                new ShowCommand(userRepository, directoriesMarkupBuilder),
                new CreateDirectoryCommand(userRepository, directoryRepository, directoriesMarkupBuilder),
                new BackCommand(userRepository,directoriesMarkupBuilder),
                new DirectoriesCommand(userRepository, directoriesMarkupBuilder),
                new DirectoryCommand(userRepository,directoryRepository, directoriesMarkupBuilder),
                new LoadFileCommand(userRepository, fileRepository, filesMarkupBuilder),
                new FileCommand(fileRepository),
                new FilesCommand(userRepository, filesMarkupBuilder)
            };
        }

        public ITelegramCommand GetCommand(Update update)
        {
            foreach (var command in commands)
            {
                if (command.CanExecute(update))
                {
                    return command;
                }
            }

            return nullCommand;
        }

        private class NullCommand : ITelegramCommand
        {
            public bool CanExecute(Update update) => true;

            public Task Execute(ITelegramBotClient client, Update update) => Task.CompletedTask;
        }
    }
}
