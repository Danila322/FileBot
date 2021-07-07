using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public abstract class SwitchCommand : CallbackQueryCommand
    {
        private readonly IRepository<UserInfo> userRepository;
        private readonly IMarkupBuilder<Directory> markupBuilder;

        public SwitchCommand(IRepository<UserInfo> userRepository, IMarkupBuilder<Directory> markupBuilder)
        {
            this.userRepository = userRepository;
            this.markupBuilder = markupBuilder;
        }

        public async override Task Execute(ITelegramBotClient client, Update update)
        {
            var userId = update.CallbackQuery.From.Id;
            var chatId = update.CallbackQuery.ChatInstance;

            var info = await userRepository.Get(userId);

            var markup = markupBuilder.Build(info.CurrentDirectory);

            await client.EditMessageTextAsync(chatId, info.BotMessageId.Value, info.CurrentDirectory.Name, replyMarkup: markup);
        }
    }
}
