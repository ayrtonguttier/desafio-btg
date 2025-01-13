using ErrorOr;
using PedidoApi.Pedidos.Contracts;
using PedidoApi.Pedidos.Queries;

namespace PedidoApi.Pedidos;

public interface IPedidoRepository: IPedidosPorClienteQuery, IQuantidadeDePedidosPorClienteQuery, IValorTotalDoPedidoQuery
{
}
