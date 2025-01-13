using System.Text.Json.Serialization;

namespace PedidoApi.Pedidos.Contracts;

public class PedidoItemResult

{
  [JsonPropertyName("produto")]
  public string Produto { get; set; } = string.Empty;
  [JsonPropertyName("quantidade")]
  public int Quantidade { get; set; }
  [JsonPropertyName("preco")]
  public decimal Preco { get; set; }
}
