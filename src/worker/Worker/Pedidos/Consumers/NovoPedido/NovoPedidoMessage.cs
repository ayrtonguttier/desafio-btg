using System.Text.Json.Serialization;

namespace Worker.Pedidos.Consumers.NovoPedido;

public class NovoPedidoMessage
{
    [JsonPropertyName("codigoPedido")]
    public int CodigoPedido { get; set; }

    [JsonPropertyName("codigoCliente")]
    public int CodigoCliente { get; set; }

    [JsonPropertyName("itens")]
    public List<NovoPedidoItemMessage> Items { get; set; } = new();
}
