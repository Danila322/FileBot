using Telegram.Bot.Types.ReplyMarkups;

namespace FileBot.MarkupBuilders
{
    public interface IMarkupBuilder<in TArg>
    {
        public InlineKeyboardMarkup Build(TArg arg);
    }
}
