using Tutorial9.Model.ProductWarehouse;

namespace Tutorial9.Services;

public interface IWarehouseService
{
    Task<int> CreateProductWarehouseAsync(CreateProductWarehouseDTO createDto);
    Task<bool> ExistForOrderAsync(int orderId);
}