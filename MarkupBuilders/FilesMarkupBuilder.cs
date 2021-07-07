using FileBot.Commands;
using FileBot.Models;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace FileBot.MarkupBuilders
{
    public class FilesMarkupBuilder : BaseMarkupBuilder
    {
        public override InlineKeyboardMarkup Build(Directory directory)
        {
            var buttons = new List<IEnumerable<InlineKeyboardButton>>();

            buttons.Add(CreateRow(nameof(CommandName.Directories), CommandName.Directories));

            foreach (var file in directory.Files)
            {
                buttons.Add(CreateRow(file.Name, CommandName.File + file.Id));
            }

            return new InlineKeyboardMarkup(buttons);
        }
    }
}
