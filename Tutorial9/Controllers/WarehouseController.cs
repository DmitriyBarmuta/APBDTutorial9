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
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex) when (ex is NoSuchProductException or NoSuchOrderException)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (AlreadyCompletedOrderException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected server internal error occurred.", detail = ex.Message });
        }
    }

    [HttpPost("procedure")]
    public async Task<IActionResult> CreateProductWarehouseWithProcedure([FromBody] CreateProductWarehouseDTO createDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var id = await _warehouseService.CreateProductWarehouseWithProcedureAsync(createDto);
            return CreatedAtAction(nameof(CreateProductWarehouseWithProcedure), new { id }, new { id });
        }
        catch (Exception ex) when (ex is InvalidProductIdException or InvalidProductIdException)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (AlreadyCompletedOrderException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected server internal error occured.", detail = ex.Message });
        }
    }
}