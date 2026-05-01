using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _repo;

    public ProductsController(ProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _repo.GetById(id);
        if (product is null)
            return NotFound(new { message = $"Mehsul {id} tapilmadi" });
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create([FromBody] Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            return BadRequest(new { message = "Ad bosh ola bilmez" });
        var created = _repo.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<Product> Update(int id, [FromBody] Product product)
    {
        var updated = _repo.Update(id, product);
        if (updated is null)
            return NotFound(new { message = $"Mehsul {id} tapilmadi" });
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (!_repo.Delete(id))
            return NotFound(new { message = $"Mehsul {id} tapilmadi" });
        return NoContent();
    }
}