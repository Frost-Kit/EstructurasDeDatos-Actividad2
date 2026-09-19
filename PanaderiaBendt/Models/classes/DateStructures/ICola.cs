
namespace PanaderiaBendt.Models.classes.DateStructures;

public interface ICola<T>
{
    bool IsEmpty();
    bool IsFull();
    void Enqueue(T elemento);
    T Dequeue();
    T Peek();
    int Count();
    int Length();

    ICollection<T> ElementsNow();
}