
namespace PanaderiaBendt.Models.classes.DateStructures;

public sealed class Pila<T> : IPila<T>
{
    private T[] _elements;
    private int _index;

    public Pila(int size = 5)
    {
        _elements = new T[size];
        _index = -1;
    }

    public bool IsEmpty() => _index == -1;
    public bool IsFull() => _index == Size() - 1;
    public int Size() => _elements.Length;
    public int Count() => _index + 1;
    
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new IndexOutOfRangeException("La Pila esta vacia");
        }
        return _elements[_index];
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new IndexOutOfRangeException("La Pila esta vacia");
        }
        return _elements[_index--];
    }

    public void Push(T element)
    {
        //_index++;
        if(IsFull())
        {
            throw new IndexOutOfRangeException("Indice fuera de rango");
        }

        _elements[++_index] = element;
    }
    
    public List<T> ToList()
    {
        List<T> list = [];
        
        for (int i = _index; i >= 0; i--)
        {
            list.Add(_elements[i]);
        }
        return list;
    }
}
