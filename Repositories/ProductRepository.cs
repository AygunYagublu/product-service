using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public List<Product> GetAll() =>
        _db.Products.ToList();

    public Product? GetById(int id) =>
        _db.Products.FirstOrDefault(p => p.Id == id);

    public Product Add(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        _db.Products.Add(product);
        _db.SaveChanges();
        return product;
    }

    public Product? Update(int id, Product updated)
    {
        var product = GetById(id);
        if (product is null) return null;

        product.Name        = updated.Name;
        product.Description = updated.Description;
        product.Price       = updated.Price;
        product.Stock       = updated.Stock;
        _db.SaveChanges();
        return product;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product is null) return false;
        _db.Products.Remove(product);
        _db.SaveChanges();
        return true;
    }
}