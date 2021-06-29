using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace FileBot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            BotSettings settings = new BotSettings(configuration);
            var client = new TelegramBotClient(settings.Key);
            client.SetWebhookAsync($"{settings.Url}/api/message/update").Wait();
            return services.AddScoped<ITelegramBotClient>(s => client);
        }
    }
}
