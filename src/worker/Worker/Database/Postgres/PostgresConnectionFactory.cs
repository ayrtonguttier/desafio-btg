using System.Data;

namespace Worker.Database.Postgres;

public class PostgresConnectionFactory : IDbConnectionFactory
{
    private readonly string _host;
    private readonly string _port;
    private readonly string _username;
    private readonly string _password;
    private readonly string _database;

    public PostgresConnectionFactory()
    {
        _host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? throw new Exception("Undefined postgres host");
        _port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? throw new Exception("Undefined postgres port");
        _username = Environment.GetEnvironmentVariable("POSTGRES_USERNAME") ?? throw new Exception("Undefined postgres username");
        _password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? throw new Exception("Undefined postgres password");
        _database = Environment.GetEnvironmentVariable("POSTGRES_DATABASE") ?? throw new Exception("Undefines postgres database");
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"User ID={_username};Password={_password};Host={_host};Port={_port};Database={_database};";
        var connection = new Npgsql.NpgsqlConnection(connectionString);
        return connection;
    }
}
