using ProductCatalog.Models;

namespace ProductCatalog.Services;

public class ProductService
{
    private readonly List<Product> _products = new() {
        new Product { Id = 1, Name = "Laptop", Price = 4500 },
        new Product { Id = 2, Name = "Klawiatura", Price = 200 },
        new Product { Id = 3, Name = "Myszka", Price = 120 }
    };

    public IEnumerable<Product> GetAll(Product p, string? nameFilter = null)
    {
        if (string.IsNullOrWhiteSpace(nameFilter))
            return _products;

        return _products
            .Where(predicate: p =>
            {
                return p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase);
            });
    }

    internal IEnumerable<Product>? GetAll(string name)
    {
        throw new NotImplementedException();
    }
}

