using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public class StartCommand : TelegramCommand
    {
        protected override string Name => CommandName.Start;

        public override Task Execute(ITelegramBotClient client, Update update)
        {
            ChatId id = update.Message.Chat.Id;
            string message = $"Hello, {update.Message.From.FirstName},\nuse {CommandName.Show} to view your files";
            return client.SendTextMessageAsync(id, message);
        }
    }
}
