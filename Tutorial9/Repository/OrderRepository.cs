using Tutorial9.Infrastructure;
using Tutorial9.Model.Order;

namespace Tutorial9.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public OrderRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<Order?> GetByConstraintsAsync(int productId, int amount, DateTime createdAt)
    {
        const string sql = """
                           SELECT *
                           FROM [Order]
                           WHERE IdProduct = @IdProduct
                           AND Amount = @Amount
                           AND CreatedAt < @CreatedAt;
                           """;

        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@IdProduct", productId);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        if (!await reader.ReadAsync()) return null;
        
        var fulfilledAtOrdinal = reader.GetOrdinal("FulfilledAt");
        DateTime? fulfilledAt = await reader.IsDBNullAsync(fulfilledAtOrdinal)
            ? null
            : reader.GetDateTime(fulfilledAtOrdinal);
        return new Order
        {
            IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
            IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
            Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            FulfilledAt = fulfilledAt
        };

    }

    public async Task<bool> FulfillOrderAsync(int orderId)
    {
        const string sql = "UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder;";

        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@IdOrder", orderId);

        await conn.OpenAsync();
        
        var result = await cmd.ExecuteNonQueryAsync();
        return result > 0;
    }
}