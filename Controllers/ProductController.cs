using Microsoft.AspNetCore.Mvc;
using product_service.Data;

[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Post(Product product)
    {
        await _productRepository.AddProductAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        await _productRepository.UpdateProductAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productRepository.DeleteProductAsync(id);
        return NoContent();
    }
}