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

[] Criar imagem docker para RabbitMQ - 1 horas
[x] Modelar base de dados relacional - 3 horas
  [x] Criar scripts de criação da base de dados - 3 horas
  [] Criar imagem docker para PostgresSQL - 1 hora
[x] Criar consumer da fila que insere no banco de dados - 3 horas
[] Criar imagem docker para o consumer da fila - 1 hora
[] Criar api em .NET - 3 horas
[] Criar imagem docker para a API .NET - 1 hora
[] Criar relatório técnico - 12 horas


### Referencias

[Tamanho do decimal em C#](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types)
[Criando imagem do postgres](https://hub.docker.com/_/postgres)
[Básico de filas com RabbitMQ](https://www.rabbitmq.com/docs/publishers#basics)




https://www.connectionstrings.com/postgresql/
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-9.0&tabs=visual-studio
https://www.rabbitmq.com/docs/queues
https://www.rabbitmq.com/docs/download
https://github.com/amantinband/error-or?tab=readme-ov-file#thenasync
https://dotnetdocs.ir/Post/43/implementing-rabbitmq-in-net-core
https://hub.docker.com/_/postgres




