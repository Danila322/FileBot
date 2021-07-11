using FileBot.Data;
using FileBot.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FileBot.Services
{
    public class DbInitializer : IInitializer
    {
        private readonly BotDbContext context;
        private ILogger<DbInitializer> logger;

        public DbInitializer(BotDbContext context, ILogger<DbInitializer> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task Initialize()
        {
            try
            {
                var migrations = await context.Database.GetPendingMigrationsAsync();
                
                if (migrations.Any())
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch(Exception e)
            {
                logger.LogError("Database initialization failed with the exception {0}", e.GetType().Name);
            }
        }
    }
}
