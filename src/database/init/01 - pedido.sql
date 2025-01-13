CREATE TABLE IF NOT EXISTS PEDIDO
(
  codigoPedido integer primary key,
  codigoCliente integer not null
);


CREATE TABLE IF NOT EXISTS ITEM_PEDIDO
(
  codigoItemPedido serial primary key,
  codigoPedido int not null REFERENCES PEDIDO(codigoPedido),
  produto text not null,
  quantidade int not null,
  preco numeric(29, 2)
)
