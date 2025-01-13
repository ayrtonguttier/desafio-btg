using System.Text.Json.Serialization;

namespace PedidoApi.Pedidos.Contracts;

public class QuantidadeDePedidosPorClienteResult
{

  public QuantidadeDePedidosPorClienteResult(int codigoCliente, int quantidade)
  {
    CodigoCliente = codigoCliente;
    QuantidadeDePedidos = quantidade;
  }

  [JsonPropertyName("codigoCliente")]
  public int CodigoCliente { get; }

  [JsonPropertyName("quantidadeDePedidos")]
  public int QuantidadeDePedidos { get; }
}
