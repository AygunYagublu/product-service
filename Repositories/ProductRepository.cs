using ProductService.Models;

namespace ProductService.Repositories;

public class ProductRepository
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public ProductRepository()
    {
        /* başlanğıc məlumatlar */
        Add(new Product { Name = "Laptop",    Description = "Gaming laptop",  Price = 1200.00m, Stock = 10 });
        Add(new Product { Name = "Mouse",     Description = "Wireless mouse", Price = 25.00m,   Stock = 50 });
        Add(new Product { Name = "Keyboard",  Description = "Mechanical",     Price = 75.00m,   Stock = 30 });
    }

    public List<Product> GetAll() => _products;

    public Product? GetById(int id) =>
        _products.FirstOrDefault(p => p.Id == id);

    public Product Add(Product product)
    {
        product.Id        = _nextId++;
        product.CreatedAt = DateTime.UtcNow;
        _products.Add(product);
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
        return product;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        if (product is null) return false;
        _products.Remove(product);
        return true;
    }
}