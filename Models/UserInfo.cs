namespace FileBot.Models
{
    public class UserInfo
    {
        public long UserId { get; set; }

        public long CurrentDirectoryId { get; set; }

        public int? BotMessageId { get; set; }

        public virtual Directory CurrentDirectory { get; set; }
    }
}
