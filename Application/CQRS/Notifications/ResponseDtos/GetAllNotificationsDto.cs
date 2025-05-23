namespace Application.CQRS.Notifications.ResponseDtos;

public class GetAllNotificationsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
}
