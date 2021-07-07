using FileBot.Commands;
using FileBot.Models;
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

        public CommandFactory(IRepository<UserInfo> repository, IMarkupBuilder markupBuilder)
        {
            commands = new ITelegramCommand[]
            {
                new StartCommand(repository),
                new ShowCommands(repository, markupBuilder)
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
