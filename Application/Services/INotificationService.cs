using Application.CQRS.Auctions.ResponseDtos;
using Domain.Entities;

namespace Application.Services;

public interface INotificationService
{
    Task SendAuctionActivatedNotificationAsync(AuctionActivatedNotificationDto data);
    Task SendAuctionStoppedNotificationAsync(int auctionId, string msgReason, User winner);
}
