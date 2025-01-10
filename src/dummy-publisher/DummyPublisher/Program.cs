using Bogus;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;


var faker = new PedidoFaker();
var pedidos = faker.GenerateLazy(10);

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: "fila", durable: false, exclusive: false, autoDelete: false, arguments: null);


foreach (var pedido in pedidos)
{
  var pedidoJson = JsonSerializer.Serialize(pedido);
  var body = Encoding.UTF8.GetBytes(pedidoJson);
  await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "pedido", body: body);
}


class PedidoFaker : Faker<Pedido>
{
  public PedidoFaker()
  {
    var itemFaker = new ItemFaker();
    RuleFor(x => x.CodigoPedido, f => f.Random.Number(int.MaxValue));
    RuleFor(x => x.CodigoCliente, f => f.Random.Number(int.MaxValue));
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
  public string Produto { get; set; } = string.Empty;
  public int Quantidade { get; set; }
  public decimal Preco { get; set; }
}

class Pedido
{
  public int CodigoPedido { get; set; }
  public int CodigoCliente { get; set; }
  public List<Item> Itens { get; set; } = new();
}
