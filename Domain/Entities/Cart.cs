using Domain.BaseEntities;

namespace Domain.Entities;

public class Cart : BaseEntity
{
    public int CartId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice => CartLines.Sum(cl => cl.TotalPrice);
    public List<CartLine> CartLines { get; set; } = new List<CartLine>();
}
