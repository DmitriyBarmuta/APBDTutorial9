using Tutorial8.Exceptions;
using Tutorial9.Exceptions;
using Tutorial9.Model.ProductWarehouse;
using Tutorial9.Repository;

namespace Tutorial9.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository, IOrderRepository orderRepository)
    {
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
    }


    public async Task<int> CreateProductWarehouseAsync(CreateProductWarehouseDTO createDto)
    {
        if (createDto.IdProduct <= 0) throw new InvalidProductIdException("Product id must be positive integer.");
        if (createDto.IdWarehouse <= 0) throw new InvalidWarehouseIdException("Warehouse id must be positive integer.");

        var order = await _orderRepository.GetByConstraintsAsync(createDto.IdProduct, createDto.Amount,
            createDto.CreatedAt); 
        if (order == null)
            throw new NoSuchOrderException($"There is no earlier order with product ID {createDto.IdProduct} and amount {createDto.Amount}.");
        if (ExistForOrderAsync(order.Id).Result)
            throw new AlreadyCompletedOrderException($"This order {order.Id} was already completed.");

        var result = await _orderRepository.FulfillOrderAsync(order.Id);
        if (!result)
            throw new DatabaseConnectionException($"Failed to fulfill the order with ID {result}");

        return await _warehouseRepository.CreateProductWarehouseAsync();
    }

    public Task<bool> ExistForOrderAsync(int orderId)
    {
        
    }
    
}