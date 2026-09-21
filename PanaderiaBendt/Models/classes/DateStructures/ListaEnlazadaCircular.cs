namespace PanaderiaBendt.Models.classes.DateStructures;

public class ListaEnlazadaCircular<T> : IListaEnlazada<T>
{
    private NodoCircular<T> _fin;
    private int _cantidad;

    public ListaEnlazadaCircular()
    {
        _fin = null;
        _cantidad = 0;
    }
    
    public int Cantidad() => _cantidad;
    public bool EsVacia() => _cantidad == 0;

    public void AgregarFin(T elemento)
    {
        NodoCircular<T> nuevoNodo = new(elemento);

        if (EsVacia()) _fin = nuevoNodo;
        else
        {
            nuevoNodo.Siguiente = _fin.Siguiente;
            _fin.Siguiente = nuevoNodo;
            _fin = nuevoNodo;
        }
        _cantidad++;
    }

    public void AgregarEn(int indice, T elemento)
    {
        
    }

    public void AgregarInicio(T elemento)
    {
        NodoCircular<T> nuevoNodo = new(elemento);

        if (EsVacia())
        {
            _fin = nuevoNodo;
        }
        else
        {
            nuevoNodo.Siguiente = _fin.Siguiente;
            _fin.Siguiente = nuevoNodo;
        }
        _cantidad++;
    }

    public int BuscarElemento(T elemento)
    {
        throw new NotImplementedException();
    }

    public void EliminarEn(int indice)
    {
        throw new NotImplementedException();
    }

    public void EliminarFin()
    {
        throw new NotImplementedException();
    }

    public void EliminarInicio()
    {
        throw new NotImplementedException();
    }

    public void MostrarDatos()
    {
        if (EsVacia()) return;

        NodoCircular<T> temporal = _fin.Siguiente;

        do
        {
            Console.Write($"[{temporal.Valor}]-> ");
            temporal = temporal.Siguiente;
        } while (temporal != _fin.Siguiente);

        Console.WriteLine("Repito");
    }

    public void VaciarLista()
    {
        _fin = null;
        _cantidad = 0;
    }


    public void EliminarValor(T elemento)
    {
        throw new NotImplementedException();
    }

    public void MostrarDatosInverso()
    {
        throw new NotImplementedException();
    }
    
    public List<T> ObtenerListaIterable()
    {
        List<T> elementos = [];
    
        NodoCircular<T>? actual = _fin.Siguiente;
        do
        {
            elementos.Add(actual.Valor);
            actual = actual.Siguiente;
        } while (!actual.Equals(_fin.Siguiente));
    
        return elementos;
    }
}
