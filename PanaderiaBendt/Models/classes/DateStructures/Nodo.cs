
namespace PanaderiaBendt.Models.classes.DateStructures;

public class Nodo<T> : INodo<T>
{
    public T Valor { get; set; }
    public Nodo<T>? Siguiente { get; set; }
    public Nodo(T valor)
    {
        Valor = valor;
        Siguiente = null;
    }

    public override string ToString()
    {
        return $"Valor: {Valor} | Siguiente: {Siguiente}";
    }
}
