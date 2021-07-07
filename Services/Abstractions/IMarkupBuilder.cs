using FileBot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace FileBot.Services.Abstractions
{
    public interface IMarkupBuilder
    {
        public InlineKeyboardMarkup BuildDirectoriesMarkup(Directory directory);

        public InlineKeyboardMarkup BuildFilesMarkup(Directory directory);
    }
}
