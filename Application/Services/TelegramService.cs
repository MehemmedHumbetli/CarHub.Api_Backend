using Microsoft.Extensions.Configuration;
using Repository.Common;

namespace Application.Services;

public interface ITelegramService
{
    Task SendMessageAsync(string chatId, string message);
    Task SaveChatIdAsync(string chatId); // Telefon zorunlu değilse bu kadar
}

public class TelegramService : ITelegramService
{
    private readonly string _botToken;
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _unitOfWork;

    public TelegramService(IConfiguration config, IUnitOfWork unitOfWork)
    {
        _botToken = config["TelegramSettings:BotToken"];
        _httpClient = new HttpClient();
        _unitOfWork = unitOfWork;
    }

    public async Task SendMessageAsync(string chatId, string message)
    {
        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";

        var data = new Dictionary<string, string>
        {
            ["chat_id"] = chatId,
            ["text"] = message
        };

        var content = new FormUrlEncodedContent(data);
        var response = await _httpClient.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Telegram API Error: {response.StatusCode}, Response: {responseContent}");
        }
    }

    public async Task SaveChatIdAsync(string chatId)
    {
        await _unitOfWork.TelegramChatRepository.SaveChatIdAsync(chatId);
        await _unitOfWork.SaveChangeAsync();
    }
}