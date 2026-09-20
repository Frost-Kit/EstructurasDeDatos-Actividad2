using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanaderiaBendt.Data;
using PanaderiaBendt.Models.classes;

namespace PanaderiaBendt.Controllers;

public class BakeryController : Controller
{
    private readonly PanaderiaDbContext _context;

    public BakeryController(PanaderiaDbContext context)
    {
        _context = context;
    }

    // -------------------------------------------------------------
    // VISTA PRINCIPAL: Muestra la DB y las 3 Estructuras en Memoria
    // -------------------------------------------------------------
    public async Task<IActionResult> Index()
    {
        // Pasamos los datos a la Vista mediante ViewBag
        ViewBag.Products = await _context.Products.ToListAsync();
        ViewBag.Customers = await _context.Customers.ToListAsync();

        // Estructuras en Memoria (obtenidas desde MemoryStore)
        ViewBag.Queue = MemoryStore.PendingOrders;
        ViewBag.Stack = MemoryStore.ActionHistory.ToList();
        ViewBag.LinkedList = MemoryStore.DeliveryRoute;

        return View();
    }

    // -------------------------------------------------------------
    // 1. COLA (Queue - FIFO): Crear Pedido y ponerlo en la Cola
    // -------------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> CreateOrder(int customerId, int productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        var customer = await _context.Customers.FindAsync(customerId);

        if (product != null && customer != null)
        {
            // A) Guardar en Base de Datos (Persistencia)
            var order = new Order
            {
                CustomerId = customerId,
                Customer = customer,
                DeliveryAddress = customer.Address,
                OrderDate = DateTime.Now,
                TotalAmount = product.Price * quantity
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // B) Agregar a la COLA en memoria (FIFO)
            MemoryStore.PendingOrders.Enqueue(order);

            // C) Registrar la acción en la PILA (Stack)
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Nuevo pedido #{order.Id} ({product.Name}) ingresó a la COLA.");
        }

        return RedirectToAction("Index");
    }

    // Atender/Despachar el primer pedido de la Cola (Enqueue -> Dequeue)
    [HttpPost]
    public IActionResult ProcessQueue()
    {
        if (MemoryStore.PendingOrders.Count() > 0)
        {
            // Extrae el primero que entró
            Order orderProcessed = MemoryStore.PendingOrders.Dequeue();

            // Registra en la Pila
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Pedido #{orderProcessed.Id} fue ATENDIDO de la Cola.");
        }

        return RedirectToAction("Index");
    }

    // -------------------------------------------------------------
    // 2. PILA (Stack - LIFO): Deshacer última acción
    // -------------------------------------------------------------
    [HttpPost]
    public IActionResult UndoAction()
    {
        if (MemoryStore.ActionHistory.Count() > 0)
        {
            // Saca el último registro ingresado
            string lastAction = MemoryStore.ActionHistory.Pop();
        }

        return RedirectToAction("Index");
    }

    // -------------------------------------------------------------
    // 3. LISTA ENLAZADA (LinkedList): Gestionar Delivery
    // -------------------------------------------------------------
    // Agregar un pedido al final del Delivery
    [HttpPost]
    public IActionResult AddToDelivery(int orderId)
    {
        var order = _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            // Métodos propios de LinkedList
            MemoryStore.DeliveryRoute.AgregarFin(order);

            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Pedido #{order.Id} añadido al final de la RUTA de delivery.");
        }

        return RedirectToAction("Index");
    }

    // Insertar pedido como URGENTE (al principio de la Lista Enlazada)
    [HttpPost]
    public IActionResult AddUrgentDelivery(int orderId)
    {
        var order = _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            // Inserción al inicio con LinkedList
            MemoryStore.DeliveryRoute.AgregarInicio(order);

            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Pedido URGENTE #{order.Id} insertado AL INICIO del delivery.");
        }

        return RedirectToAction("Index");
    }

    // Completar primera entrega del delivery (remover nodo)
    [HttpPost]
    public IActionResult CompleteDelivery()
    {
        if (MemoryStore.DeliveryRoute.Inicio != null)
        {
            var deliveredOrder = MemoryStore.DeliveryRoute.Inicio.Valor;
            
            // Remueve el primer nodo eficientemente
            MemoryStore.DeliveryRoute.EliminarInicio();

            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Delivery del pedido #{deliveredOrder.Id} ENTREGADO con éxito.");
        }

        return RedirectToAction("Index");
    }
}