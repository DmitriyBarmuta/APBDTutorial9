using Microsoft.AspNetCore.Mvc;
using Tutorial9.Exceptions;
using Tutorial9.Model.ProductWarehouse;
using Tutorial9.Services;

namespace Tutorial9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProductWarehouse([FromBody] CreateProductWarehouseDTO createDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        try
        {
            var id = await _warehouseService.CreateProductWarehouseAsync(createDto);
            return CreatedAtAction(nameof(CreateProductWarehouse), new { id }, new { id });
        }
        catch (Exception ex) when (ex is InvalidProductIdException or InvalidProductIdException)
        {
            return NotFound(new {error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred.", detail = ex.Message });
        }
    }
}