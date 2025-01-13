using ErrorOr;
using PedidoApi.Pedidos.Contracts;

namespace PedidoApi.Pedidos.Queries;

public interface IQuantidadeDePedidosPorClienteQuery
{
  Task<ErrorOr<QuantidadeDePedidosPorClienteResult>> GetQuantidadeDePedidosPorClienteAsync(int codigoCliente);

}
