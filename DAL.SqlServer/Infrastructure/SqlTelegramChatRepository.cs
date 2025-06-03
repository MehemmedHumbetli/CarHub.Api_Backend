using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure
{
    public class TelegramChatRepository : ITelegramChatRepository
    {
        private readonly AppDbContext _context;

        public TelegramChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChatIdAsync(string chatId)
        {
            var exists = await _context.TelegramChats.AnyAsync(x => x.ChatId == chatId);
            if (!exists)
            {
                await _context.TelegramChats.AddAsync(new TelegramChat { ChatId = chatId });
                
            }
        }

        public async Task<List<string>> GetAllChatIdsAsync()
        {
            return await _context.TelegramChats.Select(x => x.ChatId).ToListAsync();
        }
    }
}
