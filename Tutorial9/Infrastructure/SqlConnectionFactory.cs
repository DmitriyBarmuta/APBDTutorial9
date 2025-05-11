using Microsoft.Data.SqlClient;

namespace Tutorial9.Infrastructure;

public class SqlConnectionFactory : ISqlConnectionFactory
{

    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");        
    }

    public SqlConnection GetConnection() => new(_connectionString);
}