
namespace PanaderiaBendt.Models.classes.DateStructures;

public class ListaDobleEnlazada<T> : IListaEnlazada<T>
{
    private NodoDoble<T> _inicio;
    private NodoDoble<T> _fin;

    private int _cantidad;

    public ListaDobleEnlazada()
    {
        this._inicio = null;
        this._fin = null;
        this._cantidad = 0;
    }

    public int Cantidad() => _cantidad;

    public bool EsVacia() => _cantidad == 0;
    
    public NodoDoble<T>? Inicio => _inicio;

    public void MostrarDatos()
    {
        NodoDoble<T>? actual = _inicio;

        while (actual is not null)
        {
            if (_cantidad == 1)
            {
                Console.WriteLine($"null <- [{actual.Valor}] -> null");
                return;
            }

            if (actual.Equals(_inicio))
            {
                Console.Write($"null <- [{actual.Valor}]");
            }
            else if (actual.Equals(_fin))
            {
                Console.Write($" <=> [{actual.Valor}] -> null\n");
            }
            else
            {
                Console.Write($" <=> [{actual.Valor}]");
            }

            actual = actual.Siguiente;
        }
    }

    public void AgregarFin(T elemento)
    {
        NodoDoble<T> nuevo = new(elemento);

        if (_cantidad == 0)
        {
            _inicio = nuevo;
            _fin = nuevo;
        }
        else
        {
            _fin.Siguiente = nuevo;
            nuevo.Anterior = _fin;
            _fin = nuevo;
        }

        _cantidad++;
    }

    public void AgregarEn(int indice, T elemento)
    {
        if (indice < 0 || indice > _cantidad) return;

        if (indice == 0) AgregarInicio(elemento);
        else if (indice == _cantidad) AgregarFin(elemento);
        else
        {
            NodoDoble<T>? actual = _inicio;

            // Para posicionarse en la posicion indicada
            for (int i = 0; i < indice; i++)
                actual = actual.Siguiente;

            NodoDoble<T> nuevo = new(elemento)
            {
                Siguiente = actual,
                Anterior = actual.Anterior
            };
            actual.Anterior.Siguiente = nuevo;
            actual.Anterior= nuevo;
            _cantidad++;
        }
    }

    public void AgregarInicio(T elemento)
    {
        NodoDoble<T> nuevo = new(elemento);

        if (_cantidad == 0)
        {
            _inicio = nuevo;
            _fin = nuevo;
        }
        else
        {
            nuevo.Siguiente = _inicio;
            _inicio.Anterior = nuevo;
            _inicio = nuevo;
        }
        _cantidad++;
    }

    public int BuscarElemento(T elemento)
    {
        NodoDoble<T> actual = _inicio;
        int indice = 0;
        while (actual is not null)
        {
            if (actual.Valor.Equals(elemento))
                return indice;

            actual = actual.Siguiente;
            indice++;
        }
        return -1;
    }

    public void EliminarEn(int indice)
    {
        if (indice < 0 || indice >= _cantidad) return;

        if (indice == 0) EliminarInicio();
        else if (indice == _cantidad - 1) EliminarFin();
        else
        {
            NodoDoble<T>? actual = _inicio;
            
            // Para posicionarse en la posicion indicada
            for (int i = 0; i < indice; i++)
                actual = actual.Siguiente;
            
            actual.Anterior.Siguiente = actual.Siguiente;
            actual.Siguiente.Anterior = actual.Anterior;
            _cantidad--;
        }
    }

    public void EliminarFin()
    {
        switch (_cantidad)
        {
            case 0:
                return;
            case 1:
                VaciarLista();
                return;
            default:
                _fin.Anterior.Siguiente = null;
                _fin = _fin.Anterior;
                _cantidad--;
                break;
        }
    }

    public void EliminarInicio()
    {
        switch (_cantidad)
        {
            case 0:
                return;
            case 1:
                VaciarLista();
                return;
            default:
                _inicio.Siguiente.Anterior = null;
                _inicio = _inicio.Siguiente;
                //// Logica del inge Roger
                //_inicio = _inicio.Siguiente;
                //_inicio.Anterior = null;
                _cantidad--;
                break;
        }
    }

    public void EliminarValor(T element)
    {
        NodoDoble<T> actual = _inicio;
        int indice = 0;
        while (actual is not null)
        {
            if (_cantidad == 1)
            {
                VaciarLista();
                return;
            }

            if (actual.Valor.Equals(element))
            {
                if (actual.Anterior is null)
                    EliminarInicio();
                else if (actual.Siguiente is null)
                    EliminarFin();
                else
                {
                    actual.Anterior.Siguiente = actual.Siguiente;
                    actual.Siguiente.Anterior = actual.Anterior;
                    _cantidad--;
                }
            }

            actual = actual.Siguiente;
            indice++;
        }
    }

    public void MostrarDatosInverso()
    {
        NodoDoble<T>? actual = _fin;

        while (actual is not null)
        {
            if (_cantidad == 1)
            {
                Console.WriteLine($"null <- [{actual.Valor}] -> null");
                return;
            }

            if (actual.Equals(_fin))
            {
                Console.Write($"null <- [{actual.Valor}]");
            }
            else if (actual.Equals(_inicio))
            {
                Console.Write($" <=> [{actual.Valor}] -> null\n");
            }
            else
            {
                Console.Write($" <=> [{actual.Valor}]");
            }

            actual = actual.Anterior;
        }
    }

    public void VaciarLista()
    {
        _inicio = null;
        _fin = null;
        _cantidad = 0;
    }
    
    public List<T> ObtenerListaIterable()
    {
        List<T> elementos = [];

        NodoDoble<T>? actual = _inicio;
        while (actual != null)
        {
            elementos.Add(actual.Valor);
            actual = actual.Siguiente;
        }
    
        return elementos;
    }
}
