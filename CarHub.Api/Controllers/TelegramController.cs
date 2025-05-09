using Application.CQRS.Telegram.ResponseDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Telegram.Handlers.SendTelegramMessage;

namespace CarHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TelegramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] SendTelegramMessageDto dto)
        {
            var command = new SendTelegramMessageCommand(dto.ChatId, dto.Message);
            var result = await _mediator.Send(command);

            return result ? Ok("Mesaj gönderildi") : BadRequest("Gönderilemedi");
        }
    }
}
