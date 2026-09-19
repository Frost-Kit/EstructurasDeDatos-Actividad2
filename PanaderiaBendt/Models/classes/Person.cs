using PanaderiaBendt.Models.interfaces;

namespace PanaderiaBendt.Models.classes;

public abstract class Person : IEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Ci { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
}