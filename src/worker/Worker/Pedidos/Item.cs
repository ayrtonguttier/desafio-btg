using System;

namespace Worker.Pedidos;

public class Item
{
    public Item(string produto, int quantidade, decimal preco)
    {
        Produto = produto;
        Quantidade = quantidade;
        Preco = preco;
    }

    public string Produto { get; private set; }
    public int Quantidade { get; private set; }
    public decimal Preco { get; private set; }
}
