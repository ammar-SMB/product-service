using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly DbContext _dbContext;

    public ProductRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _dbContext.Set<Product>().ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Set<Product>().AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _dbContext.Set<Product>().Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == id);
        if (product != null)
        {
            _dbContext.Set<Product>().Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
