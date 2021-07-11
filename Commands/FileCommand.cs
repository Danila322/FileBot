using FileBot.Services.Abstractions;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace FileBot.Commands
{
    public class FileCommand : CallbackQueryCommand
    {
        private readonly IFileRepository fileRepository;

        public FileCommand(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        protected override string Name => CommandName.File;

        public async override Task Execute(ITelegramBotClient client, Update update)
        {
            var chatId = update.CallbackQuery.Message.Chat.Id;

            long fileId = long.Parse(update.CallbackQuery.Data.Substring(Name.Length));
            var file = await fileRepository.Get(fileId);

            await using(MemoryStream stream = new MemoryStream(file.Data))
            {
                InputOnlineFile input = new InputOnlineFile(stream, file.Name);
                await client.SendDocumentAsync(chatId,input);
            }
        }
    }
}
