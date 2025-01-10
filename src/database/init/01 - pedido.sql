CREATE TABLE IF NOT EXISTS PEDIDO
(
  codigoPedido int primary key,
  codigoCliente int not null
);


CREATE TABLE IF NOT EXISTS ITEM_PEDIDO
(
  codigoItemPedido int primary key,
  produto text not null,
  quantidade int not null,
  preco numeric(29, 2)
)
