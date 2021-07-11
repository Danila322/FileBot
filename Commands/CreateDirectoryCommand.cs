using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public class CreateDirectoryCommand : MessageCommand
    {
        private readonly IUserInfoRepository userRepository;
        private readonly IDirectoryRepository directoryRepository;
        private readonly IMarkupBuilder<Directory> markupBuilder;

        public CreateDirectoryCommand(IUserInfoRepository userRepository, IDirectoryRepository directoryRepository, IMarkupBuilder<Directory> markupBuilder)
        {
            this.userRepository = userRepository;
            this.directoryRepository = directoryRepository;
            this.markupBuilder = markupBuilder;
        }

        protected override string Name => CommandName.Create;

        public async override Task Execute(ITelegramBotClient client, Update update)
        {
            var userId = update.Message.From.Id;
            var chatId = update.Message.Chat.Id;

            var info = await userRepository.Get(userId);

            string directoryName = update.Message.Text.Substring(Name.Length).Trim();

            Directory directory = new Directory()
            {
                Name = directoryName,
                ParentId = info.CurrentDirectoryId
            };
            await directoryRepository.Add(directory);
            await directoryRepository.Save();

            var markup = markupBuilder.Build(info.CurrentDirectory);
            
            if(info.BotMessageId is not null)
            {
                await client.DeleteMessageAsync(chatId, info.BotMessageId.Value);
            }
            
            Message message = await client.SendTextMessageAsync(chatId, info.CurrentDirectory.Path, replyMarkup: markup);

            info.BotMessageId = message.MessageId;
            await userRepository.Save();
        }
    }
}
