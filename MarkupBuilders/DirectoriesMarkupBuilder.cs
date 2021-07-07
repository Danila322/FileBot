﻿using FileBot.Commands;
using FileBot.Models;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace FileBot.MarkupBuilders
{
    public class DirectoriesMarkupBuilder : BaseMarkupBuilder
    {
        public override InlineKeyboardMarkup Build(Directory directory)
        {
            var buttons = new List<IEnumerable<InlineKeyboardButton>>();

            if (directory.Files.Count > 0)
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
    }
}