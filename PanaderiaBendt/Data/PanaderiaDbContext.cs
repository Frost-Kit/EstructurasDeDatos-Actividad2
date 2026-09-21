using PanaderiaBendt.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace PanaderiaBendt.Data;

public class PanaderiaDbContext : DbContext
{
    public PanaderiaDbContext(DbContextOptions<PanaderiaDbContext> options) 
        : base(options)
    {
    }

    // aqui tan las tablas de la base de datos
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    /// <summary>
    /// pa darle limites a la DB, y algunas props
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");

            // ==========================================
            // CONFIGURACIÓN: Customer (Hereda de Person)
            // ==========================================
            // Configuración de ID Autoincremental (Identity)
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                .ValueGeneratedOnAdd(); // Equivale al AUTOINCREMENT / IDENTITY

            entity.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(c => c.Ci)
                .IsRequired()
                .HasMaxLength(14);

            entity.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(c => c.Address)
                .HasMaxLength(200);

            // Índice único para no repetir números de teléfono
            entity.HasIndex(c => c.PhoneNumber)
                .IsUnique();
        });
        
        // ==========================================
        // CONFIGURACIÓN: Product
        // ==========================================
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(p => p.Description)
                .HasMaxLength(250);

            entity.Property(p => p.Price)
                .HasPrecision(18, 2) // O HasColumnType("TEXT") / "REAL" para SQLite
                .IsRequired();

            entity.Property(p => p.StockQuantity)
                .HasDefaultValue(0);
        });
        
        // ==========================================
        // CONFIGURACIÓN: Order
        // ==========================================
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id)
                .ValueGeneratedOnAdd();

            entity.Property(o => o.DeliveryAddress)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            entity.Property(o => o.IsDelivered)
                .HasDefaultValue(false);

            // Relación: Customer 1 ---> N Order
            entity.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // ==========================================
        // CONFIGURACIÓN: OrderDetail
        // ==========================================
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetails");

            entity.HasKey(od => od.Id);
            entity.Property(od => od.Id)
                .ValueGeneratedOnAdd();

            entity.Property(od => od.UnitPrice)
                .HasPrecision(18, 2);

            entity.Property(od => od.Subtotal)
                .HasPrecision(18, 2);

            // Relación: Order 1 ---> N OrderDetail
            entity.HasOne<Order>()
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación: Product 1 ---> N OrderDetail
            entity.HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
    
    /// <summary>
    /// si no hay productos, pos los crea algunos datos de prueba
    /// </summary>
    /// <param name="context"></param>
    public static void Seed(PanaderiaDbContext context)
    {
        // pa asegurar que la base de datos esté creada
        context.Database.EnsureCreated();

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Rosquete", Description = "Rosquete glaseado artesanal de San Lorenzo", Price = 4.50m, StockQuantity = 100 },
                new Product { Name = "Empanada Blanqueada", Description = "Empanada dulce con blanqueado de clara de huevo", Price = 3.00m, StockQuantity = 80 },
                new Product { Name = "Hojarasca", Description = "Hojarasca tradicional crocante y con relleno de dulce de leche", Price = 2.00m, StockQuantity = 50 }
            );
        }

        if (!context.Customers.Any())
        {
            context.Customers.AddRange(
                new Customer { FullName = "Anonimo", Ci = "0000000", PhoneNumber = "00000000", Address = "SIN DELIVERY", BirthDate = new DateTime(1, 1, 1) }, // para clientes que no se quieran registrar
                new Customer { FullName = "María Benítez", Ci = "4568728", PhoneNumber = "71234567", Address = "Barrio San Roque", BirthDate = new DateTime(1995, 4, 15) },
                new Customer { FullName = "Lucía Aramayo", Ci = "9547627", PhoneNumber = "71239876", Address = "Barrio El Molino", BirthDate = new DateTime(1998, 6, 21) }
            );
        }

        context.SaveChanges();
    }
}