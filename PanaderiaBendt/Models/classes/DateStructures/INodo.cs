
namespace PanaderiaBendt.Models.classes.DateStructures;

public interface INodo<T>
{
    public T Valor { get; set; }
    public Nodo<T> Siguiente { get; set; }
}
