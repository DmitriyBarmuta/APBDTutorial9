using Tutorial9.Model.Order;

namespace Tutorial9.Repository;

public class OrderRepository : IOrderRepository
{
    public async Task<Order?> GetByConstraintsAsync(int productId, int amount, DateTime createdAt)
    {
        
    }

    public Task<bool> FulfillOrderAsync(int orderId)
    {
        
    }
}