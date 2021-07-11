using FileBot.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
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

        public BotController(ICommandFactory commandFactory, ITelegramBotClient client)
        {
            this.commandFactory = commandFactory;
            this.client = client;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if(update is null)
            {
                return BadRequest();
            }

            var command = commandFactory.GetCommand(update);
            await command.Execute(client, update);

            return Ok();
        }
    }
}
