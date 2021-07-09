using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public interface ITelegramCommand
    {
        public bool CanExecute(Update update);

        public Task Execute(ITelegramBotClient client, Update update);
    }
}
