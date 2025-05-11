using Tutorial8.Infrastructure;
using Tutorial9.Model.ProductWarehouse;

namespace Tutorial9.Repository;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public WarehouseRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<int> CreateProductWarehouseAsync(ProductWarehouse productWarehouse)
    {
        const string sql = """
                           INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                           OUTPUT INSERTED.IdProductWarehouse
                           VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt);
                           """;

        await using var conn = _sqlConnectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
        cmd.Parameters.AddWithValue("@IdOrder", productWarehouse.IdOrder);
        cmd.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
        cmd.Parameters.AddWithValue("@Price", productWarehouse.Price);
        cmd.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

        await conn.OpenAsync();
        return (int)(await cmd.ExecuteScalarAsync() ?? -1);
    }

    public async Task<bool> ExistForOrderAsync(int orderId)
    {
        const string sql = "SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder;";

        await using var conn = _sqlConnectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@IdOrder", orderId);

        await conn.OpenAsync();
        var result = (int)(await cmd.ExecuteScalarAsync() ?? 0);
        return result > 0; 
    }
}