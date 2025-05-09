namespace Domain.Entities;

public class AuctionParticipant
{
    public int AuctionId { get; set; }
    public Auction Auction { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}
