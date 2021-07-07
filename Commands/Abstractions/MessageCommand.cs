using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FileBot.Commands
{
    public abstract class MessageCommand : ITelegramCommand
    {
        protected abstract string Name { get; }

        public virtual bool CanExecute(Update update)
        {
            return update.Type == UpdateType.Message && update.Message.Text == Name;
        }

        public abstract Task Execute(ITelegramBotClient client, Update update);
    }
}
