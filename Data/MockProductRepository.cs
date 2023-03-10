namespace product_service.Data;
public class MockProductRepository : IProductRepository
{
    private readonly List<Product> _products;

    public MockProductRepository()
    {
        _products = new List<Product>()
        {
            new Product { Id = 1, Name = "Ford", Model = "F-150", IsActive = true },
            new Product { Id = 2, Name = "Chevrolet", Model = "Silverado", IsActive = false },
            new Product { Id = 3, Name = "GMC", Model = "Yukon Denali", IsActive = true },
        };
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await Task.FromResult(_products);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
    }

    public async Task AddProductAsync(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        await Task.CompletedTask;
    }

    public async Task UpdateProductAsync(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        _products[index] = product;
        await Task.CompletedTask;
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        _products.Remove(product);
        await Task.CompletedTask;
    }
}
