namespace PanaderiaBendt.Models.classes;

public sealed class Customer : Person
{
    public List<Order> Orders { get; set; } = [];
}