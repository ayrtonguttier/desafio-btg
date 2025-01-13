using System.Text.Json.Serialization;

namespace PedidoApi.Pedidos.Contracts;

public class PedidosPorClienteResult
{
  public PedidosPorClienteResult(IReadOnlyCollection<PedidoResult> pedidos)
  {
    Pedidos = pedidos;
  }
  [JsonPropertyName("pedidos")]
  public IReadOnlyCollection<PedidoResult> Pedidos { get; }
}