using Domain.Entities;

namespace Repository.Repositories;


public interface IParticipantRepository
{
    Task JoinAuction(int auctionId,int userId); 
    Task LeaveAuction(int auctionId, int userId); 
    Task<List<AuctionParticipant>> GetParticipants(int auctionId); 
}

