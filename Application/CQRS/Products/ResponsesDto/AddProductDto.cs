namespace Application.CQRS.Products.ResponsesDto;

public class AddProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
