using System.Data;

namespace Worker.Database;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();    
}
