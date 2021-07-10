using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public class ShowCommands : MessageCommand
    {
        private readonly IUserInfoRepository repository;
        private readonly IMarkupBuilder<Directory> markupBuilder;

        public ShowCommands(IUserInfoRepository repository, IMarkupBuilder<Directory> markupBuilder)
        {
            this.repository = repository;
            this.markupBuilder = markupBuilder;
        }

        protected override string Name => CommandName.Show;

        public async override Task Execute(ITelegramBotClient client, Update update)
        {
            var userId = update.Message.From.Id;
            var chatId = update.Message.Chat.Id;
            var info = await repository.Get(userId);

            if(info.BotMessageId is not null)
            {
                await client.DeleteMessageAsync(chatId, info.BotMessageId.Value);
            }

            var markup = markupBuilder.Build(info.CurrentDirectory);
            Message message = await client.SendTextMessageAsync(chatId, info.CurrentDirectory.Name, replyMarkup: markup);

            info.BotMessageId = message.MessageId;
            await repository.Save();
        }
    }
}
