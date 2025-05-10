using System.Reflection;
using Microsoft.Data.SqlClient;
using Tutorial8.Infrastructure;

namespace Tutorial8.Config;

public class DatabaseInitializer
{

    private readonly ISqlConnectionFactory _connectionFactory;
    
    public DatabaseInitializer(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        await using var connection = _connectionFactory.GetConnection();
        await connection.OpenAsync();


        var createSql = ReadEmbeddedSql("DatabaseCreate.sql");
        var fillSql = ReadEmbeddedSql("DatabaseFill.sql");

        await using var createCommand = connection.CreateCommand();
        createCommand.CommandText = createSql;
        await createCommand.ExecuteNonQueryAsync();

        var checkCmd = connection.CreateCommand();
        checkCmd.CommandText = "SELECT COUNT(*) FROM Product";
        var result = (int)(await checkCmd.ExecuteScalarAsync() ?? 0);

        if (result == 0)
        {
            await using var fillCommand = connection.CreateCommand();
            fillCommand.CommandText = fillSql;
            await fillCommand.ExecuteNonQueryAsync();
        }
    }

    private static string ReadEmbeddedSql(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly
            .GetManifestResourceNames()
            .FirstOrDefault(x => x.Contains(fileName));

        if (resourceName == null)
            throw new FileNotFoundException($"Couldn't find embedded resource: {fileName}");

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}