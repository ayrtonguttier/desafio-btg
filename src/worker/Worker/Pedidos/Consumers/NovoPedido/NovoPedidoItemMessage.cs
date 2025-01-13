using System;
using System.Text.Json.Serialization;

namespace Worker.Pedidos.Consumers.NovoPedido;

public class NovoPedidoItemMessage
{
    [JsonPropertyName("produto")]
    public string Produto { get; set; } = string.Empty;

    [JsonPropertyName("quantidade")]
    public int Quantidade { get; set; }

    [JsonPropertyName("preco")]
    public decimal Preco { get; set; }
}
