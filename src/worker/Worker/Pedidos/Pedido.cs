namespace Worker.Pedidos;
public class Pedido
{
    private readonly List<Item> items = [];
    public Pedido(int codigoPedido, int codigoCliente)
    {
        CodigoPedido = codigoPedido;
        CodigoCliente = codigoCliente;
    }
    public int CodigoPedido { get; }
    public int CodigoCliente { get; }
    public IReadOnlyCollection<Item> Itens => items.ToList();

    public void AddItens(IEnumerable<Item> itens)
    {
        items.AddRange(itens);
    }
}
