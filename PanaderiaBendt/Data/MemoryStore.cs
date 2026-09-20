using PanaderiaBendt.Models.classes;
using PanaderiaBendt.Models.classes.DateStructures;

namespace PanaderiaBendt.Data;

public static class MemoryStore
{
    // 1. COLA (Queue - FIFO): Para los pedidos pendientes en mostrador/horno
    public static Cola<Order> PendingOrders = new(10);

    // 2. PILA (Stack - LIFO): Para el historial de acciones y "deshacer"
    public static Pila<string> ActionHistory = new(100);

    // 3. LISTA ENLAZADA (LinkedList): Para las paradas dinámicas del delivery por Tarija
    public static ListaEnlazada<Order> DeliveryRoute = new();
}