using System;
using ErrorOr;
using PedidoApi.Pedidos.Contracts;

namespace PedidoApi.Pedidos.Queries;

public interface IPedidosPorClienteQuery
{
  Task<ErrorOr<PedidosPorClienteResult>> GetPedidosPorClienteAsync(int codigoCliente);

}
