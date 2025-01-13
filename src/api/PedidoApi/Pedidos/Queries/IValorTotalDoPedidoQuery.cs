using System;
using ErrorOr;
using PedidoApi.Pedidos.Contracts;

namespace PedidoApi.Pedidos.Queries;

public interface IValorTotalDoPedidoQuery
{
  Task<ErrorOr<ValorTotalDoPedidoResult>> GetValorTotalPedidoAsync(int codigoPedido);
}
