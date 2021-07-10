using FileBot.Models;
using FileBot.Services;
using FileBot.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace FileBot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            BotSettings settings = new BotSettings();
            configuration.Bind(nameof(BotSettings), settings);
            var client = new TelegramBotClient(settings.Key);
            client.SetWebhookAsync($"{settings.Url}/api/message/update").Wait();
            return services.AddSingleton<ITelegramBotClient>(client);
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserInfoRepository, UserInfoRepository>()
                .AddScoped<IDirectoryRepository, DirectoryRepository>()
                .AddScoped<IFileRepository, FileRepoitory>();
        }
    }
}
