using System.Text.Json.Serialization;

namespace PedidoApi.Pedidos.Contracts;

public class ValorTotalDoPedidoResult
{
  public ValorTotalDoPedidoResult(int codigoPedido, decimal valorTotal)
  {
    CodigoPedido = codigoPedido;
    ValorTotal = valorTotal;
  }
  [JsonPropertyName("codigoPedido")]
  public int CodigoPedido { get; }

  [JsonPropertyName("valorTotal")]
  public decimal ValorTotal { get; }
}
