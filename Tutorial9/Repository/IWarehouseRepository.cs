using Tutorial9.Model.ProductWarehouse;
using System.Threading;

namespace Tutorial9.Repository;

public interface IWarehouseRepository
{
    Task<int> CreateProductWarehouseAsync(ProductWarehouse productWarehouse, CancellationToken cancellationToken);
    Task<bool> ExistForOrderAsync(int orderId, CancellationToken cancellationToken);
    Task<int> CallCreationOfProductWarehouseProcedureAsync(CreateProductWarehouseDTO createDto, CancellationToken cancellationToken);
}