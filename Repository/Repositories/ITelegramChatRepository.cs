namespace Repository.Repositories;


public interface ITelegramChatRepository
{
    Task SaveChatIdAsync(string chatId);
    Task<List<string>> GetAllChatIdsAsync();
}
