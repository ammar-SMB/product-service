using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _ProductRepository;

    public ProductsController(IProductRepository ProductRepository)
    {
        _ProductRepository = ProductRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var Products = await _ProductRepository.GetAllAsync();
        return Ok(Products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var Product = await _ProductRepository.GetByIdAsync(id);
        if (Product == null)
        {
            return NotFound();
        }
        return Ok(Product);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Product Product)
    {
        await _ProductRepository.AddAsync(Product);
        return CreatedAtAction(nameof(GetById), new { id = Product.Id }, Product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Product Product)
    {
        if (id != Product.Id)
        {
            return BadRequest();
        }
        await _ProductRepository.UpdateAsync(Product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _ProductRepository.DeleteAsync(id);
        return NoContent();
    }
}