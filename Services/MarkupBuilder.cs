using FileBot.Commands;
using FileBot.Models;
using FileBot.Services.Abstractions;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace FileBot.Services
{
    public class MarkupBuilder : IMarkupBuilder
    {
        public InlineKeyboardMarkup BuildDirectoriesMarkup(Directory directory)
        {
            var buttons = new List<IEnumerable<InlineKeyboardButton>>();
            
            if(directory.Files.Count > 0)
            {
                buttons.Add(CreateRow(nameof(CommandName.Files), CommandName.Files));
            }

            if (directory.Parent is not null)
            {
                buttons.Add(CreateRow(nameof(CommandName.Back), CommandName.Back));
            }
            
            buttons.Add(CreateRow(nameof(CommandName.Create), CommandName.Create));

            foreach (var dir in directory.Directories)
            {
                buttons.Add(CreateRow(dir.Name, CommandName.Directory + dir.Id));
            }
            
            return new InlineKeyboardMarkup(buttons);
        }

        public InlineKeyboardMarkup BuildFilesMarkup(Directory directory)
        {
            var buttons = new List<IEnumerable<InlineKeyboardButton>>();

            buttons.Add(CreateRow(nameof(CommandName.Directories), CommandName.Directories));
            
            foreach (var file in directory.Files)
            {
                buttons.Add(CreateRow(file.Name, CommandName.File + file.Id));
            }

            return new InlineKeyboardMarkup(buttons);
        }

        private IEnumerable<InlineKeyboardButton> CreateRow(string text, string data)
        {
            return new[] { new InlineKeyboardButton() { Text = text, CallbackData = data } };
        }
    }
}
