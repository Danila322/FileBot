using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FileBot.Extensions;
using FileBot.Services.Abstractions;
using FileBot.Services;
using FileBot.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace FileBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<BotDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DataBaseConnection"));
                    options.UseLazyLoadingProxies();
                })
                .AddRepositories()
                .AddScoped<IInitializer, DbInitializer>()
                .AddScoped<IMarkupBuilderFactory, MarkupBuilderFactory>()
                .AddScoped<ICommandFactory, CommandFactory>()
                .AddTelegramBot(Configuration)
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
