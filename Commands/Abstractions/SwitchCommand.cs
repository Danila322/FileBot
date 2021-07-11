using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public abstract class SwitchCommand : CallbackQueryCommand
    {
        private readonly IUserInfoRepository userRepository;
        private readonly IMarkupBuilder<Directory> markupBuilder;

        public SwitchCommand(IUserInfoRepository userRepository, IMarkupBuilder<Directory> markupBuilder)
        {
            this.userRepository = userRepository;
            this.markupBuilder = markupBuilder;
        }

        public async override Task Execute(ITelegramBotClient client, Update update)
        {
            var userId = update.CallbackQuery.From.Id;
            var chatId = update.CallbackQuery.Message.Chat.Id;

            var info = await userRepository.Get(userId);

            var markup = markupBuilder.Build(info.CurrentDirectory);

            await client.EditMessageTextAsync(chatId, info.BotMessageId.Value, info.CurrentDirectory.Path, replyMarkup: markup);
        }
    }
}
