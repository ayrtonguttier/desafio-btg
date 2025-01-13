using Worker.Database;
using Worker.Database.Postgres;
using Worker.Pedidos;
using Worker.Pedidos.Consumers.NovoPedido;
using Worker.Pedidos.Repositories;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IDbConnectionFactory, PostgresConnectionFactory>();
builder.Services.AddSingleton<IPedidoRepository, PedidoRepository>();
builder.Services.AddHostedService<NovoPedidoConsumer>();

var host = builder.Build();
host.Run();
