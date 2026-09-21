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
    
    /// <summary>
    /// Para cargar la vista/pagina principal
    /// </summary>
    /// <returns>un view</returns>
    public async Task<IActionResult> Index()
    {
        ViewBag.Products = await _context.Products.ToListAsync();
        ViewBag.Customers = await _context.Customers.ToListAsync();

        // Estructuras pasadas a la vista
        ViewBag.Queue = MemoryStore.PendingOrders;
        ViewBag.Stack = MemoryStore.ActionHistory.ToList();
        ViewBag.DeliveryList = MemoryStore.DeliveryRoute;      // Lista Simple
        ViewBag.BakingList = MemoryStore.BakingSchedule;        // Lista Doble
        ViewBag.PromosList = MemoryStore.FeaturedProducts;      // Lista Circular

        return View();
    }

    // =========================================================
    // 1. COLA & PILA
    // =========================================================
    /// <summary>
    /// Crear una nueva orden, guardarla en la DB con el context, y la "encuela" en PendingOrders
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <returns>nada ejecuta y vuelve la view principal</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrder(int customerId, int productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        var customer = await _context.Customers.FindAsync(customerId);

        if (product != null && customer != null)
        {
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

            MemoryStore.PendingOrders.Enqueue(order);
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Nuevo pedido #{order.Id} ingresó a la COLA.");
        }

        return RedirectToAction("Index");
    }
    
    /// <summary>
    /// Desencolar al pedido u orden si se le atendio en la tienda
    /// </summary>
    /// <returns>nada ejecuta y vuelve la view principal</returns>
    [HttpPost]
    public IActionResult ProcessQueue()
    {
        // Validacion Rúbrica: Verificar si la cola está vacía
        if (!MemoryStore.PendingOrders.IsEmpty())
        {
            Order orderProcessed = MemoryStore.PendingOrders.Dequeue();
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Pedido #{orderProcessed.Id} fue ATENDIDO.");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UndoAction()
    {
        if (!MemoryStore.ActionHistory.IsEmpty())
        {
            string lastAction = MemoryStore.ActionHistory.Pop();
        }

        return RedirectToAction("Index");
    }

    // =========================================================
    // 2. LISTA ENLAZADA SIMPLE (Ruta Delivery)
    // =========================================================
    [HttpPost]
    public IActionResult AddToDelivery(int orderId)
    {
        var order = _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            MemoryStore.DeliveryRoute.AgregarFin(order);
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Pedido #{order.Id} añadido a la RUTA.");
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult AddUrgentDelivery(int orderId)
    {
        var order = _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            MemoryStore.DeliveryRoute.AgregarInicio(order);
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Pedido #{order.Id} añadido a la RUTA ¡URGENTE!.");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult CompleteDelivery()
    {
        // Validación Rúbrica: Caso especial lista vacía
        if (!MemoryStore.DeliveryRoute.EsVacia())
        {
            var deliveredOrder = MemoryStore.DeliveryRoute.Inicio!.Valor;
            MemoryStore.DeliveryRoute.EliminarInicio();
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Delivery #{deliveredOrder.Id} ENTREGADO.");
        }

        return RedirectToAction("Index");
    }

    // =========================================================
    // 3. LISTA DOBLE ENLAZADA (Cronograma de Horneado)
    // =========================================================
    [HttpPost]
    public IActionResult AddBakingItem(string panNombre, int posicion = -1)
    {
        if (string.IsNullOrWhiteSpace(panNombre)) return RedirectToAction("Index");

        if (posicion == 0)
        {
            MemoryStore.BakingSchedule.AgregarInicio(panNombre);
        }
        else
        {
            MemoryStore.BakingSchedule.AgregarFin(panNombre);
        }

        MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Lote '{panNombre}' programado para hornear.");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveBakingItem(int index)
    {
        // Validación Rúbrica: Índices fuera de rango o lista vacía
        if (!MemoryStore.BakingSchedule.EsVacia() && index >= 0 && index < MemoryStore.BakingSchedule.Cantidad())
        {
            MemoryStore.BakingSchedule.EliminarEn(index);
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Lote en posición #{index} retirado del horno.");
        }

        return RedirectToAction("Index");
    }

    // =========================================================
    // 4. LISTA ENLAZADA CIRCULAR (Promociones Destacadas)
    // =========================================================
    [HttpPost]
    public async Task<IActionResult> AddFeaturedProduct(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            MemoryStore.FeaturedProducts.AgregarFin(product);
            MemoryStore.ActionHistory.Push($"[{DateTime.Now:HH:mm:ss}] Producto '{product.Name}' añadido a Promociones Destacadas.");
        }

        return RedirectToAction("Index");
    }
}