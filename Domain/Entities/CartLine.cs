using Domain.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class CartLine : BaseEntity
{
    [Key]
    public int Id { get; set; }  // CartLine için birincil anahtar

    [ForeignKey("Cart")]
    public int CartId { get; set; } // CartId, Cart tablosuna foreign key

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => UnitPrice * Quantity;
    public Product Product { get; set; }


}
