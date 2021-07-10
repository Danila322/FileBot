using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public class BackCommand : CallbackQueryCommand
    {
        private readonly IUserInfoRepository repository;
        private readonly IMarkupBuilder<Directory> markupBuilder;

        public BackCommand(IUserInfoRepository repository, IMarkupBuilder<Directory> markupBuilder)
        {
            this.repository = repository;
            this.markupBuilder = markupBuilder;
        }

        protected override string Name => CommandName.Back;

        public async override Task Execute(ITelegramBotClient client, Update update)
        {
            var userId = update.CallbackQuery.From.Id;
            var chatId = update.CallbackQuery.ChatInstance; 

            var info = await repository.Get(userId);

            info.CurrentDirectory = info.CurrentDirectory.Parent;
            info.CurrentDirectoryId = info.CurrentDirectory.Id;

            var markup = markupBuilder.Build(info.CurrentDirectory);

            await client.EditMessageTextAsync(chatId, info.BotMessageId.Value, info.CurrentDirectory.Name, replyMarkup: markup);
            await repository.Save();
        }
    }
}
