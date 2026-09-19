
namespace PanaderiaBendt.Models.classes.DateStructures;

public interface IPila<T>
{
    void Push(T element);

    T Pop();
    T Peek();

    int Size();
    bool IsEmpty();
    bool IsFull();
    int Count();
}
