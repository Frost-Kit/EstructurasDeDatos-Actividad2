
namespace PanaderiaBendt.Models.classes.DateStructures;

public class ListaDobleEnlazadaCircular<T> : IListaEnlazada<T>
{
    private NodoDobleCircular<T> _fin;

    private int _cantidad;

    public ListaDobleEnlazadaCircular()
    {
        this._fin = null;
        this._cantidad = 0;
    }

    public int Cantidad() => _cantidad;

    public bool EsVacia() => _cantidad == 0;

    public void AgregarEn(int indice, T elemento)
    {
        if (indice < 0 || indice > _cantidad) return;

        if (indice == 0) AgregarInicio(elemento);
        else if (indice == _cantidad) AgregarFin(elemento);
        else
        {
            NodoDobleCircular<T>? actual = _fin.Siguiente;

            // Para posicionarse en la posicion indicada
            for (int i = 1; i < indice; i++)
                actual = actual.Siguiente;

            NodoDobleCircular<T> nuevo = new(elemento)
            {
                Siguiente = actual,
                Anterior = actual.Anterior
            };
            actual.Anterior.Siguiente = nuevo;
            actual.Anterior = nuevo;
            _cantidad++;
        }
    }

    public void AgregarFin(T elemento)
    {
        NodoDobleCircular<T> nuevo = new(elemento);

        if (_cantidad == 0)
            _fin = nuevo;
        else
        {
            nuevo.Siguiente = _fin.Siguiente;
            nuevo.Anterior = _fin;
            _fin.Siguiente.Anterior = nuevo;
            _fin.Siguiente = nuevo;
            _fin = nuevo;
        }

        _cantidad++;
    }

    public void AgregarInicio(T elemento)
    {
        NodoDobleCircular<T> nuevo = new(elemento);

        if (_cantidad == 0)
        {
            _fin = nuevo;
        }
        else
        {
            nuevo.Siguiente = _fin.Siguiente;
            nuevo.Anterior = _fin;
            _fin.Siguiente.Anterior = nuevo;
            _fin.Siguiente = nuevo;
            //_fin = nuevo;
        }
        _cantidad++;
    }

    public int BuscarElemento(T elemento)
    {
        NodoDobleCircular<T> actual = _fin.Siguiente;
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
            NodoDobleCircular<T>? actual = _fin.Siguiente;

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
                _fin.Anterior.Siguiente = _fin.Siguiente;
                _fin.Siguiente.Anterior = _fin.Siguiente;
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
                _fin.Anterior.Siguiente = _fin.Siguiente;
                _fin.Siguiente.Anterior = _fin.Siguiente;
                //_fin = _fin.Anterior;
                _cantidad--;
                break;
        }
    }
    // TODO: Para hacer nosotros
    public void EliminarValor(T element)
    {
        //NodoDobleCircular<T> actual = _fin.Siguiente;
        //int indice = 0;
        //while (actual is not null)
        //{
        //    if (_cantidad == 1)
        //    {
        //        VaciarLista();
        //        return;
        //    }

        //    if (actual.Valor.Equals(element))
        //    {
        //        if (actual.Anterior is null)
        //            EliminarInicio();
        //        else if (actual.Siguiente is null)
        //            EliminarFin();
        //        else
        //        {
        //            actual.Anterior.Siguiente = actual.Siguiente;
        //            actual.Siguiente.Anterior = actual.Anterior;
        //            _cantidad--;
        //        }
        //    }

        //    actual = actual.Siguiente;
        //    indice++;
        //}
    }

    public void MostrarDatos()
    {
        NodoDobleCircular<T>? actual = _fin.Siguiente;

        do
        {
            if (_cantidad == 1)
            {
                Console.WriteLine($"Inicio <=> [{actual.Valor}] <=> Fin");
                return;
            }

            if (actual.Equals(_fin.Siguiente))
            {
                Console.Write($"Inicio <=> [{actual.Valor}]");
            }
            else if (actual.Equals(_fin))
            {
                Console.Write($" <=> [{actual.Valor}] <=> Fin\n");
            }
            else
            {
                Console.Write($" <=> [{actual.Valor}]");
            }

            actual = actual.Siguiente;
        } while (!actual.Equals(_fin.Siguiente)) ;
    }

    public void MostrarDatosInverso()
    {
        NodoDobleCircular<T>? actual = _fin;

        do
        {
            if (_cantidad == 1)
            {
                Console.WriteLine($"Fin <=> [{actual.Valor}] <=> Inicio");
                return;
            }

            if (actual.Equals(_fin))
            {
                Console.Write($"Fin <=> [{actual.Valor}]");
            }
            else if (actual.Equals(_fin.Siguiente))
            {
                Console.Write($" <=> [{actual.Valor}] <=> Inicio\n");
            }
            else
            {
                Console.Write($" <=> [{actual.Valor}]");
            }

            actual = actual.Anterior;
        } while (!actual.Equals(_fin));
    }

    public void VaciarLista()
    {
        _fin = null;
        _cantidad = 0;
    }
    
    public List<T> ObtenerListaIterable()
    {
        List<T> elementos = [];
    
        NodoDobleCircular<T>? actual = _fin.Siguiente;
        do
        {
            elementos.Add(actual.Valor);
            actual = actual.Siguiente;
        } while (!actual.Equals(_fin.Siguiente));
    
        return elementos;
    }
}
