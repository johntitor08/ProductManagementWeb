using Microsoft.EntityFrameworkCore;
using ProductManagementWeb.Data;
using ProductManagementWeb.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlite("Data Source=products.db"));

var app = builder.Build();

// wwwroot için
app.UseDefaultFiles();
app.UseStaticFiles();

// Ürünleri listele
app.MapGet("/products", async (ProductContext db) =>
    await db.Products.ToListAsync());

// Ürün ekle
app.MapPost("/products", async (ProductContext db, Product product) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});

// Ürün güncelle
app.MapPut("/products/{id}", async (int id, ProductContext db, Product inputProduct) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = inputProduct.Name;
    product.Price = inputProduct.Price;
    product.Stock = inputProduct.Stock;

    await db.SaveChangesAsync();
    return Results.Ok(product);
});

// Ürün sil
app.MapDelete("/products/{id}", async (int id, ProductContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
