using Domain.BaseEntities;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public int UnitsOnOrder { get; set; }
    public string Description { get; set; }

}
