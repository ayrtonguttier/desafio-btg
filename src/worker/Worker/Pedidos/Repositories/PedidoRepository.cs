using ErrorOr;
using Worker.Database;
using Dapper;
using System.Data;

namespace Worker.Pedidos.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ILogger<PedidoRepository> _logger;
    private readonly IDbConnectionFactory _connectionFactory;
    public PedidoRepository(ILogger<PedidoRepository> logger, IDbConnectionFactory connectionFactory)
    {
        _logger = logger;
        _connectionFactory = connectionFactory;
    }
    public async Task<ErrorOr<Created>> CreatePedidoAsync(Pedido pedido)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            var transaction = connection.BeginTransaction();
            var pedidoResult = await CreatePedidoAsync(connection, transaction, pedido);
            if (pedidoResult.IsError)
            {
                transaction.Rollback();
                return pedidoResult.Errors;
            }

            var itensResult = await CreateItensPedidoAsync(connection, transaction, pedidoResult.Value, pedido.Itens);
            if (itensResult.IsError)
            {
                transaction.Rollback();
                return itensResult.Errors;
            }
            transaction.Commit();
            return Result.Created;
        }
        catch (Exception ex)
        {
            return Error.Failure("PedidoRepository.ConnectionError", ex.ToString());
        }
    }

    private async Task<ErrorOr<Created>> CreateItensPedidoAsync(IDbConnection connection, IDbTransaction transaction, int idPedido, IReadOnlyCollection<Item> itens)
    {
        var insertItemPedidoSql = "insert into item_pedido (codigoPedido, produto, quantidade, preco) values(@codigoPedido, @produto, @quantidade, @preco)";
        var errors = new List<Error>();
        foreach (var item in itens)
        {
            try
            {
                await connection.ExecuteAsync(insertItemPedidoSql, new { codigoPedido = idPedido, produto = item.Produto, quantidade = item.Quantidade, preco = item.Preco });
            }
            catch (Exception ex)
            {
                errors.Add(Error.Failure("PedidoRepository.InsertPedidoItemError", "Erro ao inserir item do pedido"));
                _logger.LogError(ex, "Erro ao registrar itens.");
            }
        }

        if (errors.Any())
        {
            return errors;
        }

        return Result.Created;
    }

    private async Task<ErrorOr<int>> CreatePedidoAsync(IDbConnection connection, IDbTransaction transaction, Pedido pedido)
    {
        try
        {
            var insertPedidoSql = "insert into pedido (codigoPedido, codigoCliente) values (@codigoPedido, @codigoCliente) returning codigoPedido";
            var result = await connection.QuerySingleAsync<int>(insertPedidoSql, new { codigoPedido = pedido.CodigoPedido, codigoCliente = pedido.CodigoCliente }, transaction);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao inserir pedido");
            return Error.Failure("PedidoRepository.InsertPedidoError", "Erro ao registrar pedido");
        }
    }
}
