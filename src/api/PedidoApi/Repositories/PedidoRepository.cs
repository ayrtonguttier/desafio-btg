using Dapper;
using ErrorOr;
using PedidoApi.Database;
using PedidoApi.Pedidos;
using PedidoApi.Pedidos.Contracts;

namespace PedidoApi.Repositories;

public class PedidoRepository : IPedidoRepository
{
  private readonly ILogger<PedidoRepository> _logger;
  private readonly IDbConnectionFactory _dbConnectionFactory;

  public PedidoRepository(ILogger<PedidoRepository> logger, IDbConnectionFactory dbConnectionFactory)
  {
    _logger = logger;
    _dbConnectionFactory = dbConnectionFactory;
  }

  public async Task<ErrorOr<PedidosPorClienteResult>> GetPedidosPorClienteAsync(int codigoCliente)
  {
    try
    {
      Console.WriteLine("Consultando {0}", codigoCliente);
      using var connection = _dbConnectionFactory.CreateConnection();
      var queryString = "select * from pedido where codigoCliente = @codigoCliente";
      var result = await connection.QueryAsync<PedidoResult>(queryString, new { codigoCliente });
      if (!result.Any())
      {
        return Error.NotFound("Pedido.NaoEncontrado", "Não foram encontrados pedidos para este cliente");
      }

      var pedidos = result.ToList();
      foreach (var pedido in pedidos)
      {
        var itens = await GetPedidoItensAsync(pedido.CodigoPedido);
        if (itens.IsError)
          return itens.Errors;

        pedido.LoadItens(itens.Value);
      }
      return new PedidosPorClienteResult(result.ToList());
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Erro ao consultar pedidos do cliente {codigoCliente}", codigoCliente);
      return Error.Failure("PedidoRepository.GetPedidosPorClienteAsync", "Erro ao consultar pedidos, tente novamente mais tarde.");
    }
  }


  private async Task<ErrorOr<IReadOnlyCollection<PedidoItemResult>>> GetPedidoItensAsync(int codigoPedido)
  {
    try
    {
      using var connection = _dbConnectionFactory.CreateConnection();
      var queryString = "select * from item_pedido where codigoPedido = @codigoPedido";
      var result = await connection.QueryAsync<PedidoItemResult>(queryString, new { codigoPedido });

      return result.ToList();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Erro ao consultar itens do pedido {codigoPedido}", codigoPedido);
      return Error.Failure("GetPedidoItens", "Não foi possível consultar os itens do pedido, tente novamente mais tarde");
    }
  }

  public async Task<ErrorOr<QuantidadeDePedidosPorClienteResult>> GetQuantidadeDePedidosPorClienteAsync(int codigoCliente)
  {
    try
    {
      using var connection = _dbConnectionFactory.CreateConnection();
      var queryString = "select count(1) from pedido where codigoCliente = @codigoCliente";
      var result = await connection.QueryAsync<int>(queryString, new { codigoCliente });

      if (!result.Any())
      {
        return Error.NotFound("Pedidos.NaoEncontrado", "Não foram encontrados pedidos para este cliente");
      }

      return new QuantidadeDePedidosPorClienteResult(codigoCliente, result.Single());
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Erro ao consultar quantidade de pedidos do cliente {codigoCliente}", codigoCliente);
      return Error.Failure("PedidoRepository.GetQuantidadeDePedidosPorClienteAsync", "Não foi possível consultar a quantidade de pedidos, tente novamente mais tarde.");
    }
  }

  public async Task<ErrorOr<ValorTotalDoPedidoResult>> GetValorTotalPedidoAsync(int codigoPedido)
  {
    using var connection = _dbConnectionFactory.CreateConnection();
    var queryString = "select sum(quantidade * preco) from item_pedido where codigoPedido = @codigoPedido";
    var result = await connection.QuerySingleAsync<decimal?>(queryString, new { codigoPedido });
    if (result is null)
    {
      return Error.NotFound("Pedido.NaoEncontrado", "Não foram encontrados itens para este pedido");
    }

    return new ValorTotalDoPedidoResult(codigoPedido, result.Value);
  }

  public async Task<ErrorOr<IReadOnlyCollection<ClienteQueFezPedidoResult>>> GetClientesQueFizeramPedidosAsync()
  {
    try
    {
      using var connection = _dbConnectionFactory.CreateConnection();
      var queryString = "select distinct codigoCliente from pedido;";
      var result = await connection.QueryAsync<int>(queryString);

      if (!result.Any())
      {
        return Error.NotFound("Pedido.NaoEncontrado", "Não foram encontrados clientes que fizeram pedidos");
      }

      return result.Select(x => new ClienteQueFezPedidoResult(x)).ToList();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Erro ao consultar clientes que fizeram pedidos");
      return Error.Failure("Pedido.FalharAoConsultarClientes", "Não foi possível encontrar os clientes, tente novamente mais tarde");
    }
  }
}
