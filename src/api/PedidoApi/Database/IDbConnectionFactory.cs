using System.Data;

namespace PedidoApi.Database;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();    
}
