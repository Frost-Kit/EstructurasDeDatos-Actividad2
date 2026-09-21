namespace PanaderiaBendt.Models.classes.DateStructures;

public class NodoDobleCircular<T>
{
    public T Valor { get; set; }
    public NodoDobleCircular<T>? Siguiente { get; set; }
    public NodoDobleCircular<T>? Anterior { get; set; }

    public NodoDobleCircular(T valor)
    {
        this.Valor = valor;
        this.Siguiente = this;
        this.Anterior = this;
    }
}
