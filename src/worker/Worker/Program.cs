using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: "fila", durable: false, exclusive: false, autoDelete: false, arguments: null);


var consumer = new AsyncEventingBasicConsumer(channel);


consumer.ReceivedAsync += async (ch, ea) => {
  var body = ea.Body.ToArray();
  var json = Encoding.UTF8.GetString(body);
  Console.WriteLine(json);
  await channel.BasicAckAsync(ea.DeliveryTag, false);
};

await channel.BasicConsumeAsync(queue: "fila", autoAck: false, consumer: consumer);

await Task.Delay(TimeSpan.FromSeconds(30));
