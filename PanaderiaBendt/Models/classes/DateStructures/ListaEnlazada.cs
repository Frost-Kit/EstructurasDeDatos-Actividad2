
namespace PanaderiaBendt.Models.classes.DateStructures;

public class ListaEnlazada<T> : IListaEnlazada<T>
{
    private Nodo<T> _inicio;
    private int _cantidad;

    public ListaEnlazada()
    {
        _inicio = null;
        _cantidad = 0;
    }
    
    public Nodo<T>? Inicio => _inicio;
    
    public int Cantidad() => _cantidad;

    public bool EsVacia() => _cantidad == 0;

    public void AgregarFin(T elemento)
    {
        Nodo<T> nuevoNodo = new(elemento);
        if (EsVacia())
            _inicio = nuevoNodo;
        else
        {
            Nodo<T> temporal = _inicio;
            while (temporal.Siguiente is not null)
            {
                temporal = temporal.Siguiente;
            }
            temporal.Siguiente = nuevoNodo;
        }
        _cantidad++;
    }

    public void AgregarInicio(T elemento)
    {
        Nodo<T> nuevoNodo = new(elemento);

        if (EsVacia()) 
            _inicio = nuevoNodo;
        else
        {
            nuevoNodo.Siguiente = _inicio;
            _inicio = nuevoNodo;
        }            
        _cantidad++;
    }

    /// <summary>
    /// Este metodo me retorna el indice del valor encontrado
    /// </summary>
    /// <param name="elemento">Valor dentro de un nodo</param>
    /// <returns>indice del valor encontrado, o sino existe -1</returns>
    /// <exception cref="NotImplementedException"></exception>
    public int BuscarElemento(T elemento)
    {
        int indice = -1;

        if (EsVacia()) return -1;

        Nodo<T> temporal = _inicio;

        while(temporal is not null)
        {
            indice++;
            if (temporal.Valor.Equals(elemento))
            {
                return indice;
            }

            temporal = temporal.Siguiente;
        }

        return -1;
    }

    public void EliminarFin()
    {
        if (EsVacia()) return;

        if (_cantidad == 1)
        {
            VaciarLista();
            return;
        }

        Nodo<T> temporal = _inicio;

        while(temporal is not null)
        {
            if(temporal.Siguiente.Siguiente is null)
            {
                temporal.Siguiente = null;
                _cantidad--;
                return;
            }

            temporal = temporal.Siguiente;
        }
    }


    public void EliminarInicio()
    {
        if (EsVacia()) return;

        _inicio = _inicio.Siguiente;
        _cantidad--;
    }

    public void EliminarEn(int indice)
    {
        if (indice < 0 || indice > _cantidad)
        {
            Console.WriteLine("Indice fuera de rango");
            return;
        }

        if (indice == 0)
        {
            EliminarInicio();
        }
        else if (indice == _cantidad-1)
        {
            EliminarFin();
        }
        else
        {
            Nodo<T> temporal = _inicio;

            for (int i = 0; i < indice - 1; i++) temporal = temporal.Siguiente;

            temporal.Siguiente = temporal.Siguiente.Siguiente;
            _cantidad--;
        }
    }

    public void VaciarLista()
    {
        _inicio = null;
        _cantidad = 0;
    }

    public void MostrarDatos()
    {
        Nodo<T> temporal = _inicio;

        while (temporal is not null)
        {
            Console.Write($"[{temporal.Valor}]-> ");
            temporal = temporal.Siguiente;
        }
        Console.WriteLine("null");
    }

    public void AgregarEn(int indice, T elemento)
    {
        if (indice < 0 || indice > _cantidad)
        {
            Console.WriteLine("Indice fuera de rango");
            return;
        }

        if (indice == 0)
        {
            AgregarInicio(elemento);
        }
        else if (indice == _cantidad)
        {
            AgregarFin(elemento);
        }
        else
        {
            Nodo<T> nuevoNodo = new(elemento);
            Nodo<T> temporal = _inicio;

            for (int i = 0; i < indice; i++) temporal = temporal.Siguiente;

            nuevoNodo.Siguiente = temporal.Siguiente;
            temporal.Siguiente = nuevoNodo;
            _cantidad++;
        }
    }
    // TODO: Falta hacer
    public void VoltearNodos()
    {
        Nodo<int> nodoA = new(2);
        Nodo<int> nodoB = new(7);

        nodoA.Siguiente = nodoB;
        nodoB.Siguiente = nodoA;

        Console.WriteLine($"A -> {nodoA}\n");
    }

    public void EliminarValor(T elemento)
    {
        throw new NotImplementedException();
    }

    public void MostrarDatosInverso()
    {
        throw new NotImplementedException();
    }
}
