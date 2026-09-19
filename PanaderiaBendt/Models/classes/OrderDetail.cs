using PanaderiaBendt.Models.interfaces;

namespace PanaderiaBendt.Models.classes;

public sealed class OrderDetail : IEntity
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }

    // Propiedad de navegación
    public Product? Product { get; set; }
}