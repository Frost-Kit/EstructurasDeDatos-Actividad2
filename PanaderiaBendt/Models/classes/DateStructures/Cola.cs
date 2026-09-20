
namespace PanaderiaBendt.Models.classes.DateStructures;

public sealed class Cola<T> : ICola<T>
{
    private T[] _elements;
    private int _start;
    private int _end;
    private int _quantityElements;

    public Cola(int size)
    {
        _elements = new T[size];
        _quantityElements = 0;
        _start = 0;
        _end = 0;
    }
    
    public int Count() => _quantityElements;

    public int Length() => _elements.Length;

    public bool IsEmpty() => _quantityElements == 0;

    public bool IsFull() => _quantityElements == Length();

    public void Enqueue(T elemento)
    {
        if (IsFull())
        {
            Console.WriteLine("No hay espacio en la estructura!");
            return;
        }

        _elements[_end++] = elemento;
        if (_end == Length()) _end = 0;
        _quantityElements++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new Exception("La Cola esta llena!");
        }
        _quantityElements--;
        T aux = _elements[_start++];
        
        if (_start == Length()) _start = 0;
        
        return aux;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new IndexOutOfRangeException("Sin elementos que mostrar!");
        }
        return _elements[_start];
    }

    public ICollection<T> ElementsNow()
    {
        if (IsEmpty())
        {
            throw new NotImplementedException("No hay elementos para devolver!");
        }

        List<T> listReturn = [];
        int x = _start;
        for (int i = 0; i < _quantityElements; i++)
        {
            if(x == Length())
            {
                x = 0;
            }
            listReturn.Add(_elements[x++]);
        }

        return listReturn;
    }
}
