using ErrorOr;

namespace Worker.Pedidos;

public interface IPedidoRepository
{
    Task<ErrorOr<Created>> CreatePedidoAsync(Pedido pedido);
}
