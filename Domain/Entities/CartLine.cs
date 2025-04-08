using Domain.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class CartLine : BaseEntity
{


    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => UnitPrice * Quantity;

    public Product Product { get; set; }
    public Cart Cart { get; set; }
}
