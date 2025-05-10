using Tutorial9.Model.Order;

namespace Tutorial9.Repository;

public interface IOrderRepository
{
    Task<Order?> GetByConstraintsAsync(int productId, int amount, DateTime createdAt);
    Task<bool> FulfillOrderAsync(int orderId);
}