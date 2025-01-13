using System.Text.Json.Serialization;

namespace PedidoApi.Pedidos.Contracts;

public class PedidoResult
{
  [JsonPropertyName("codigoPedido")]
  public int CodigoPedido { get; set; }

  [JsonPropertyName("codigoCliente")]
  public int CodigoCliente { get; set; }

  [JsonPropertyName("itens")]
  public IReadOnlyCollection<PedidoItemResult> Itens { get; private set; } = [];


  public void LoadItens(IReadOnlyCollection<PedidoItemResult> itens)
  {
    Itens = itens;
  }

}
