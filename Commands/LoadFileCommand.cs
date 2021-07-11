using FileBot.MarkupBuilders;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using File = FileBot.Models.File;
using Directory = FileBot.Models.Directory;

namespace FileBot.Commands
{
    public class LoadFileCommand : ITelegramCommand
    {
        private readonly IUserInfoRepository userRepository;
        private readonly IFileRepository fileRepository;
        private readonly IMarkupBuilder<Directory> markupBuilder;

        public LoadFileCommand(IUserInfoRepository userRepository, IFileRepository fileRepository, IMarkupBuilder<Directory> markupBuilder)
        {
            this.userRepository = userRepository;
            this.fileRepository = fileRepository;
            this.markupBuilder = markupBuilder;
        }

        public bool CanExecute(Update update)
        {
            return update.Type == UpdateType.Message && update.Message.Document is not null;
        }

        public async Task Execute(ITelegramBotClient client, Update update)
        {
            var chatId = update.Message.From.Id;

            Document document = update.Message.Document;
            UserInfo info = await userRepository.Get(update.Message.From.Id);

            await DownloadFile(client, document, info.CurrentDirectory);

            var markup = markupBuilder.Build(info.CurrentDirectory);
            await client.DeleteMessageAsync(chatId, info.BotMessageId.Value);
            Message message = await client.SendTextMessageAsync(chatId, info.CurrentDirectory.Path, replyMarkup: markup);

            info.BotMessageId = message.MessageId;
            await userRepository.Save();
        }

        private async Task DownloadFile(ITelegramBotClient client, Document document, Directory destination)
        {
            await using (MemoryStream stream = new MemoryStream())
            {
                await client.GetInfoAndDownloadFileAsync(document.FileId, stream);

                var file = new File()
                {
                    Name = document.FileName,
                    DirectoryId = destination.Id,
                    Data = stream.ToArray()
                };

                await fileRepository.Add(file);
                await fileRepository.Save();
            }
        }
    }
}
