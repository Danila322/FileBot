using FileBot.MarkupBuilders;
using FileBot.Models;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace FileBot.MarkupBuilders
{
    public abstract class BaseMarkupBuilder : IMarkupBuilder<Directory>
    {
        public abstract InlineKeyboardMarkup Build(Directory arg);

        protected IEnumerable<InlineKeyboardButton> CreateRow(string text, string data)
        {
            return new[] { new InlineKeyboardButton() { Text = text, CallbackData = data } };
        }
    }
}
