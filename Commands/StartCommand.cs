using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public class StartCommand : MessageCommand
    {
        private readonly IUserInfoRepository repository;

        public StartCommand(IUserInfoRepository repository)
        {
            this.repository = repository;
        }

        protected override string Name => CommandName.Start;

        public override async Task Execute(ITelegramBotClient client, Update update)
        {
            User user = update.Message.From;
            ChatId id = update.Message.Chat.Id;
            
            if (!await repository.Exist(user.Id))
            {
                UserInfo info = new UserInfo() { UserId = user.Id, CurrentDirectory = new Directory() };
                await repository.Add(info);
                await repository.Save();
            }
            
            string message = $"Hello, {user.FirstName}, use {CommandName.Show} to view your files";
            await client.SendTextMessageAsync(id, message);
        }
    }
}
