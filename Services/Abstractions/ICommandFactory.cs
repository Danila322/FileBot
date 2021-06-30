using FileBot.Commands;
using Telegram.Bot.Types;

namespace FileBot.Services.Abstractions
{
    public interface ICommandFactory
    {
        public ITelegramCommand GetCommand(Update update);
    }
}
