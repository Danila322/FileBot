using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public class DirectoryCommand : CallbackQueryCommand
    {
        private readonly IRepository<UserInfo> userRepository;
        private readonly IRepository<Directory> directoryRepository;
        private readonly IMarkupBuilder<Directory> markupBuilder;

        public DirectoryCommand(IRepository<UserInfo> userRepository, IRepository<Directory> directoryRepository, IMarkupBuilder<Directory> markupBuilder)
        {
            this.userRepository = userRepository;
            this.directoryRepository = directoryRepository;
            this.markupBuilder = markupBuilder;
        }

        protected override string Name => CommandName.Directory;

        public async override Task Execute(ITelegramBotClient client, Update update)
        {
            var userId = update.CallbackQuery.From.Id;
            var chatId = update.CallbackQuery.ChatInstance;
            var directoryId = long.Parse(update.CallbackQuery.Data.Substring(Name.Length));

            var info = await userRepository.Get(userId);
            var directory = await directoryRepository.Get(directoryId);
            info.CurrentDirectory = directory;
            info.CurrentDirectoryId = directory.Id;

            var markup = markupBuilder.Build(info.CurrentDirectory);

            await client.EditMessageTextAsync(chatId, info.BotMessageId.Value, info.CurrentDirectory.Name, replyMarkup: markup);
            await userRepository.Save();
        }
    }
}
