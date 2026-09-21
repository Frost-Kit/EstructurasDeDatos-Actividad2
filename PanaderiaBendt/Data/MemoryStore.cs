using PanaderiaBendt.Models.classes;
using PanaderiaBendt.Models.classes.DateStructures;

namespace PanaderiaBendt.Data;

public static class MemoryStore
{
    // Cola (FIFO): Atención de Pedidos
    public static Cola<Order> PendingOrders = new(10);

    // Pila (LIFO): Historial de acciones
    public static Pila<string> ActionHistory = new(100);

    // 1. LISTA SIMPLE: Ruta de Deliveries
    public static ListaEnlazada<Order> DeliveryRoute = new();

    // 2. LISTA DOBLE ENLAZADA: Orden de Horneado de Lotes
    public static ListaDobleEnlazada<string> BakingSchedule = new();

    // 3. LISTA CIRCULAR: Carrusel de Productos Destacados / Promociones
    public static ListaEnlazadaCircular<Product> FeaturedProducts = new();
}