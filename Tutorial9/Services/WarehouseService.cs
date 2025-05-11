using Tutorial9.Exceptions;
using Tutorial9.Model.ProductWarehouse;
using Tutorial9.Repository;

namespace Tutorial9.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }


    public async Task<int> CreateProductWarehouseAsync(CreateProductWarehouseDTO createDto)
    {
        if (createDto.IdProduct <= 0) throw new InvalidProductIdException("Product id must be positive integer.");
        if (createDto.IdWarehouse <= 0) throw new InvalidWarehouseIdException("Warehouse id must be positive integer.");

        var order = await _orderRepository.GetByConstraintsAsync(createDto.IdProduct, createDto.Amount,
            createDto.CreatedAt); 
        if (order == null)
            throw new NoSuchOrderException($"There is no earlier order with product ID {createDto.IdProduct} and amount {createDto.Amount}.");
        if (await _warehouseRepository.ExistForOrderAsync(order.IdOrder))
            throw new AlreadyCompletedOrderException($"This order {order.IdOrder} was already completed.");

        var result = await _orderRepository.FulfillOrderAsync(order.IdOrder);
        if (!result)
            throw new DatabaseConnectionException($"Failed to fulfill the order with ID {result}");

        var product = await _productRepository.GetByIdAsync(createDto.IdProduct);
        if (product == null)
            throw new NoSuchOrderException($"There is no product with product ID {createDto.IdProduct}.");

        var productWarehouse = new ProductWarehouse
        {
            IdWarehouse = createDto.IdWarehouse,
            IdProduct = createDto.IdProduct,
            IdOrder = order.IdOrder,
            Amount = createDto.Amount,
            Price = product.Price * createDto.Amount,
            CreatedAt = order.CreatedAt
        };

        return await _warehouseRepository.CreateProductWarehouseAsync(productWarehouse);
    }

    public async Task<int> CreateProductWarehouseWithProcedureAsync(CreateProductWarehouseDTO createDto)
    {
        return await _warehouseRepository.CallCreationOfProductWarehouseProcedureAsync(createDto);
    }
}