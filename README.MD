# Processo seletivo BTG - Ayrton Guttier

## O pedido


Crie um micro serviço que consuma dados de uma fila RabbitMQ e grave os dados para conseguir listar as informações:
a. Valor total do pedido
b. Quantidade de Pedidos por Cliente
c. Lista de pedidos realizados por cliente

``` json
{
  "codigoPedido": 1001,
  "codigoCliente":1,
  "itens": [
    {
      "produto": "lápis",
      "quantidade": 100,
      "preco": 1.10
    },
    {
      "produto": "caderno",
      "quantidade": 10,
      "preco": 1.00
    }
  ]
}
```


Crie uma API REST, em que permita o consultar as seguintes informações:
a. Valor total do pedido
b. Quantidade de Pedidos por Cliente
c. Lista de pedidos realizados por cliente

## Planejamento



### Atividades

[] Criar imagem docker para RabbitMQ
[] Modelar base de dados relacional
[] Criar scripts de criação da base de dados
[] Criar imagem docker para PostgresSQL
[] Criar consumer da fila que insere no banco de dados
[] Criar imagem docker para o consumer da fila
[] Criar api em .NET
[] Criar imagem docker para a API .NET
[] Criar diagrama de arquitetura
[] Criar diagrama de implantação
[] Criar diagrama de infra
