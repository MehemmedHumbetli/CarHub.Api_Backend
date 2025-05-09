using Domain.Entities;

namespace Repository.Repositories;


public interface IParticipantRepository
{
    Task JoinAuction(AuctionParticipant participant); 
    Task LeaveAuction(int auctionId, int userId); 
    Task<List<User>> GetParticipants(int auctionId); 
    Task<bool> IsParticipant(int auctionId, int userId); 
}

