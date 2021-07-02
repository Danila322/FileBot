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

        public CommandFactory()
        {
            commands = new[]
            {
                new StartCommand()
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
