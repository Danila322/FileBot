using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Commands
{
    public abstract class CallbackQueryCommand : ITelegramCommand
    {
        protected abstract string Name { get; }

        public virtual bool CanExecute(Update update)
        {
            var query = update.CallbackQuery;
            return query is not null && query.Data.StartsWith(Name);
        }

        public abstract Task Execute(ITelegramBotClient client, Update update);
    }
}
