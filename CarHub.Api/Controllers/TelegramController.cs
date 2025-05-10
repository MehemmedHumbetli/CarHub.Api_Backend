using Application.CQRS.Telegram.Handlers;
using Application.CQRS.Telegram.ResponseDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static Application.CQRS.Telegram.Handlers.SaveTelegramChatId;
using static Application.CQRS.Telegram.Handlers.SendTelegramMessage;
using static Application.CQRS.Telegram.Handlers.SendToAllTelegramChat;

namespace CarHub.Api.Controllers;

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

    [HttpPost("save")]
    public async Task<IActionResult> SaveChatId([FromBody] string chatId)
    {
        var command = new SaveTelegramChatIdCommand(chatId);
        var result = await _mediator.Send(command);
        return result ? Ok("Chat ID kaydedildi") : BadRequest("Kaydedilemedi");
    }

    // Tüm chat ID’lere mesaj gönderme: api/telegram/send-to-all
    [HttpPost("send-to-all")]
    public async Task<IActionResult> SendToAll([FromBody] string message)
    {
        var command = new SendToAllTelegramChatsCommand(message);
        var result = await _mediator.Send(command);
        return result ? Ok("Mesajlar gönderildi") : BadRequest("Gönderilemedi");
    }


    [HttpPost("webhook")]
    public async Task<IActionResult> Receive([FromBody] TelegramUpdateDto update)
    {
        if (update?.Message?.Chat?.Id == 0)
        {
            return Ok(); // chat id yoksa boş geç
        }

        var chatId = update.Message.Chat.Id.ToString();
        var text = update.Message.Text;

        Console.WriteLine($"Gelen mesaj: {text}, Chat ID: {chatId}");

        if (text == "/start")
        {
            await _mediator.Send(new SaveTelegramChatIdCommand(chatId));
            Console.WriteLine("Chat ID başarıyla kaydedildi.");
        }

        return Ok();
    }

}
