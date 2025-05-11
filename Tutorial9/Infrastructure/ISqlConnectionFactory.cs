using Microsoft.Data.SqlClient;

namespace Tutorial9.Infrastructure;

public interface ISqlConnectionFactory
{
    SqlConnection GetConnection();
}