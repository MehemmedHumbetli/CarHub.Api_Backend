namespace Domain.Entities;

public class RunningAuction
{
    public int AuctionId { get; set; }
    public decimal CurrentPrice { get; set; }
    public string LastBidderUserName { get; set; }
    public DateTime ExpireAt { get; set; }

    public void ResetTimer() => ExpireAt = DateTime.UtcNow.AddSeconds(15);

    public int RemainingSeconds => Math.Max((int)(ExpireAt - DateTime.UtcNow).TotalSeconds, 0);

    public bool IsExpired => DateTime.UtcNow >= ExpireAt;
}