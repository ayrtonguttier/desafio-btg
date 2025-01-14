using System.Text;
using System.Text.Json;
using ErrorOr;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker.Pedidos.Consumers.NovoPedido;

public class NovoPedidoConsumer : BackgroundService
{
  private readonly ILogger<NovoPedidoConsumer> _logger;
  private readonly string _hostname;
  private readonly string _username;
  private readonly string _password;
  private readonly string _queue;
  private IChannel? channel;
  private readonly IPedidoRepository _pedidoRepository;

  public NovoPedidoConsumer(ILogger<NovoPedidoConsumer> logger, IPedidoRepository pedidoRepository)
  {
    _hostname = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? throw new Exception("Undefined host");
    _username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? throw new Exception("Undefined username");
    _password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? throw new Exception("Undefined password");
    _queue = Environment.GetEnvironmentVariable("RABBITMQ_QUEUE") ?? throw new Exception("Undefined queue");
    _pedidoRepository = pedidoRepository;

    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    try
    {

      var factory = new ConnectionFactory { HostName = _hostname, UserName = _username, Password = _password };
      var connection = await factory.CreateConnectionAsync(cancellationToken: stoppingToken);
      channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
      await channel.QueueDeclareAsync(queue: _queue, durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken: stoppingToken);
      var consumer = new AsyncEventingBasicConsumer(channel);
      consumer.ReceivedAsync += ConsumeMessageAsync;
      await channel.BasicConsumeAsync(_queue, false, consumer, stoppingToken);

      while (!stoppingToken.IsCancellationRequested)
      {
      }
    }
    catch (Exception)
    {
      Environment.ExitCode = 1;
      throw;
    }
  }

  private async Task ConsumeMessageAsync(object ch, BasicDeliverEventArgs ea)
  {
    _logger.LogInformation("ConsumeMessage");
    try
    {
      var body = ea.Body.ToArray();
      var json = Encoding.UTF8.GetString(body);
      var message = JsonSerializer.Deserialize<NovoPedidoMessage>(json) ?? throw new Exception("Mensagem inválida");

      var pedido = new Pedido(message.CodigoPedido, message.CodigoCliente);
      pedido.AddItens(message.Items.Select(x => new Item(x.Produto, x.Quantidade, x.Preco)));

      var result = await _pedidoRepository.CreatePedidoAsync(pedido);
      if (!result.IsError)
      {
        if (channel is null)
        {
          throw new Exception("RabbitMQ channel is not open");
        }
        await channel.BasicAckAsync(ea.DeliveryTag, false);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Erro ao consumir mensagem {DeliveryTag}", ea.DeliveryTag);
      throw;
    }
  }
}
