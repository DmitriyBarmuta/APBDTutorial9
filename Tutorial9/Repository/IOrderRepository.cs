using Tutorial9.Model.Order;

namespace Tutorial9.Repository;

public interface IOrderRepository
{
    Task<Order?> GetByConstraintsAsync(int productId, int amount, DateTime createdAt, CancellationToken cancellationToken);
    Task<bool> FulfillOrderAsync(int orderId, CancellationToken cancellationToken);
}