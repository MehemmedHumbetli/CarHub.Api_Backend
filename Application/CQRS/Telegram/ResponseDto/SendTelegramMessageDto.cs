namespace Application.CQRS.Telegram.ResponseDto;

public class SendTelegramMessageDto
{
    public string ChatId { get; set; }
    public string Message { get; set; }
}
