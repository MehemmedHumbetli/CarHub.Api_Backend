namespace Application.CQRS.AuctionParticipants.ResponseDtos;

public class JoinAuctionDto
{
    public int AuctionId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinedAt { get; set; }
    public string Message { get; set; }
}

