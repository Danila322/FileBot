using FileBot.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FileBot.Controllers.Api
{
    [Route("api/message/update")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ICommandFactory commandFactory;
        private readonly ITelegramBotClient client;
        private readonly ILogger<BotController> logger;

        public BotController(ICommandFactory commandFactory, ITelegramBotClient client, ILogger<BotController> logger)
        {
            this.commandFactory = commandFactory;
            this.client = client;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if(update is null)
            {
                logger.LogError("Parametr {0} is null", nameof(update));
                return BadRequest();
            }

            var command = commandFactory.GetCommand(update);

            try
            {
                await command.Execute(client, update);
            }
            catch(Exception ex)
            {
                logger.LogError("{0} occured when executing the command {1}:\n{2}",
                    ex.GetType().Name, command.GetType().Name, ex.Message);
            }

            return Ok();
        }
    }
}
