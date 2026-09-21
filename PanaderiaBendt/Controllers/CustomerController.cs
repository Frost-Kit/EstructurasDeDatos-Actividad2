using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanaderiaBendt.Data;
using PanaderiaBendt.Models.classes;

namespace PanaderiaBendt.Controllers;

public class CustomerController : Controller
{
    private readonly PanaderiaDbContext _appDBContext;

    public CustomerController(PanaderiaDbContext appDBContext)
    {
        _appDBContext  = appDBContext;
    }
     
    // GET
    [HttpGet]
    public async Task<IActionResult> Customers()
    {
        ViewBag.ActualCustomers = await _appDBContext.Customers.ToListAsync();
        return View();
    }
     
    [HttpPost]
    public async Task<IActionResult> NewCustomer(Customer customer)
    {
        await _appDBContext.Customers.AddAsync(customer);
        await _appDBContext.SaveChangesAsync();
        return RedirectToAction(nameof(Customers));
    }
}