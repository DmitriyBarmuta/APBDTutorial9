using Tutorial9.Model.ProductWarehouse;

namespace Tutorial9.Repository;

public interface IWarehouseRepository
{
    Task<int> CreateProductWarehouseAsync(ProductWarehouse productWarehouse);
    Task<bool> ExistForOrderAsync(int orderId);
}