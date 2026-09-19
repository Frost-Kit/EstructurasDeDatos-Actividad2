using PanaderiaBendt.Models.interfaces;

namespace PanaderiaBendt.Models.classes;

public sealed class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // "Rosquete Tradicional", "Empanada Blanqueada"
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } // Price in BOB (Bolivianos)
    public int StockQuantity { get; set; }
    
}