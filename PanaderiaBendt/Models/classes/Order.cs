using PanaderiaBendt.Models.interfaces;

namespace PanaderiaBendt.Models.classes;

public sealed class Order : IEntity
{
    public int Id { get; set; }
        
    // Foreign Key & Navigation Property to Customer
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public bool IsDelivered { get; set; } = false;

    // Navigation Property: One order has many items
    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}