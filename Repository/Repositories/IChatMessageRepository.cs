using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories;

public interface IChatMessageRepository
{
    Task AddAsync(ChatMessage message);
    Task<IEnumerable<ChatMessage>> GetUserMessages(int receiverId);

    Task<IEnumerable<ChatMessage>> GetMessagesByUserIdsAsync(int senderId, int receiverId);
}
