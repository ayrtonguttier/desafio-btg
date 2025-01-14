using System;
using System.Text.Json.Serialization;

namespace PedidoApi.Pedidos.Contracts;

public class ClienteQueFezPedidoResult
{
  public ClienteQueFezPedidoResult(int codigoCliente)
  {
    CodigoCliente = codigoCliente;
  }
  [JsonPropertyName("codigoCliente")]
  public int CodigoCliente { get; }
}
