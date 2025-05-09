using Microsoft.Extensions.Configuration;

namespace Application.Services;

public interface ITelegramService
{
    Task SendMessageAsync(string chatId, string message);
}

public class TelegramService : ITelegramService
{
    private readonly string _botToken;
    private readonly HttpClient _httpClient;

    public TelegramService(IConfiguration config)
    {
        _botToken = config["TelegramSettings:BotToken"];
        _httpClient = new HttpClient();
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
}