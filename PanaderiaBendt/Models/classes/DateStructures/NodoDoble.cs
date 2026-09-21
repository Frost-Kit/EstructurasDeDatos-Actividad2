namespace PanaderiaBendt.Models.classes.DateStructures;

public class NodoDoble<T>
{
    public T Valor { get; set; }
    public NodoDoble<T>? Siguiente { get; set; }
    public NodoDoble<T>? Anterior { get; set; }

    public NodoDoble(T valor)
    {
        this.Valor = valor;
        this.Siguiente = null;
        this.Anterior = null;
    }
}
