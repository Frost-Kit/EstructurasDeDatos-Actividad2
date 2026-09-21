namespace PanaderiaBendt.Models.classes.DateStructures;

public class NodoCircular<T>
{
    public T Valor { get; set; }
    public NodoCircular<T> Siguiente { get; set; }
    public NodoCircular(T valor)
    {
        Valor = valor;
        Siguiente = this;
    }

    public override string ToString()
    {
        return $"Valor: {Valor} | Siguiente: {Siguiente}";
    }
}
