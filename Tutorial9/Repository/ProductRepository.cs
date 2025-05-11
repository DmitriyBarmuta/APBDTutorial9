using Tutorial9.Infrastructure;
using Tutorial9.Model.Product;

namespace Tutorial9.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public ProductRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        const string sql  = "SELECT * FROM Product WHERE IdProduct = @ProductId";

        await using var conn = _connectionFactory.GetConnection();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@ProductId", id);

        await conn.OpenAsync(cancellationToken);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken)) return null;
        
        return new Product
        {
            IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Description = reader.GetString(reader.GetOrdinal("Description")),
            Price = reader.GetDecimal(reader.GetOrdinal("Price"))
        };
    }
}