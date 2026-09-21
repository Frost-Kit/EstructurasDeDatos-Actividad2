using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanaderiaBendt.Data;
using PanaderiaBendt.Models.classes;

namespace PanaderiaBendt.Controllers;

public class ProductController : Controller
{
    private readonly PanaderiaDbContext _appDBContext;

    public ProductController(PanaderiaDbContext appDBContext)
    {
        _appDBContext  = appDBContext;
    }
     
    // GET
    [HttpGet]
    public async Task<IActionResult> Products()
    {
        ViewBag.ActualProducts = await _appDBContext.Products.ToListAsync();
        return View();
    }
     
    [HttpPost]
    public async Task<IActionResult> NewProduct(Product product)
    {
        await _appDBContext.Products.AddAsync(product);
        await _appDBContext.SaveChangesAsync();
        return RedirectToAction(nameof(Products));
    }
}