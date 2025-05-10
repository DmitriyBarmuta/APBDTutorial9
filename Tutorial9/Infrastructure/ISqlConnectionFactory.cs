using Microsoft.Data.SqlClient;

namespace Tutorial8.Infrastructure;

public interface ISqlConnectionFactory
{
    SqlConnection GetConnection();
}