using Microsoft.Extensions.Configuration;

namespace FileBot
{
    public class BotSettings
    {
        public BotSettings(IConfiguration configuration)
        {
            Url = configuration["Url"];
            Key = configuration["Key"];
        }

        public string Url { get; }

        public string Key { get; }
    }
}
