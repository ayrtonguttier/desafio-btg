using ErrorOr;
using PedidoApi.Pedidos.Contracts;

namespace PedidoApi.Pedidos.Queries;

public interface IClientesQueFizeramPedidosQuery
{

  Task<ErrorOr<IReadOnlyCollection<ClienteQueFezPedidoResult>>> GetClientesQueFizeramPedidosAsync();
}
