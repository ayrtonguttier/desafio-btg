using Bogus;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;

var run = true;
var fila = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE") ?? "novopedido";
var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
var user = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "superuser";
var pass = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "supersenha";

var faker = new PedidoFaker();

var factory = new ConnectionFactory { HostName = host, UserName = user, Password = pass };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: fila, durable: false, exclusive: false, autoDelete: false, arguments: null);

Console.CancelKeyPress += (object? sender, ConsoleCancelEventArgs args) =>
{
  run = false;
};

while (run)
{
  var pedidos = faker.GenerateLazy(1000);
  foreach (var pedido in pedidos)
  {
    var pedidoJson = JsonSerializer.Serialize(pedido);
    var body = Encoding.UTF8.GetBytes(pedidoJson);
    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: fila, body: body);
  }
  await Task.Delay(TimeSpan.FromSeconds(30));
}


class PedidoFaker : Faker<Pedido>
{
  public PedidoFaker()
  {
    var i = 0;
    var itemFaker = new ItemFaker();
    RuleFor(x => x.CodigoPedido, f => i++);
    RuleFor(x => x.CodigoCliente, f => f.Random.Number(10));
    RuleFor(x => x.Itens, itemFaker.GenerateBetween(1, 100));
  }
}

class ItemFaker : Faker<Item>
{
  public ItemFaker()
  {
    RuleFor(x => x.Produto, f => f.Commerce.ProductName());
    RuleFor(x => x.Quantidade, f => f.Random.Number(200));
    RuleFor(x => x.Preco, f => Math.Round(f.Random.Decimal(99999), 2));
  }
}


class Item
{
  [JsonPropertyName("produto")]
  public string Produto { get; set; } = string.Empty;
  [JsonPropertyName("quantidade")]
  public int Quantidade { get; set; }
  [JsonPropertyName("preco")]
  public decimal Preco { get; set; }
}

class Pedido
{
  [JsonPropertyName("codigoPedido")]
  public int CodigoPedido { get; set; }
  [JsonPropertyName("codigoCliente")]
  public int CodigoCliente { get; set; }
  [JsonPropertyName("itens")]
  public List<Item> Itens { get; set; } = new();
}
