## O pedido

Crie um micro serviço que consuma dados de uma fila RabbitMQ e grave os dados para conseguir listar as informações:

- Valor total do pedido
- Quantidade de Pedidos por Cliente
- Lista de pedidos realizados por cliente

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

- Valor total do pedido
- Quantidade de Pedidos por Cliente
- Lista de pedidos realizados por cliente

## Docs

- [Fila](./src/queue/readme.md)
- [Database](./src/database/readme.md)
- [Worker](./src/worker/readme.md)

## Planejamento


### Atividades

- [x] Criar imagem docker para RabbitMQ - 1 horas
- [x] Modelar base de dados relacional - 3 horas
- [x] Criar scripts de criação da base de dados - 3 horas
- [x] Criar imagem docker para PostgresSQL - 1 hora
- [x] Criar consumer da fila que insere no banco de dados - 3 horas
- [x] Criar imagem docker para o consumer da fila - 1 hora
- [ ] Criar api em .NET - 3 horas
- [ ] Criar imagem docker para a API .NET - 1 hora
- [ ] Criar relatório técnico - 12 horas

#### Comentários
- Criar imagem docker para RabbitMQ - Consumi menos tempo. Como não é algo que faço costumeiramente, estimei mais tempo do que foi necessário.
- Modelar base de dados relacional - Consumi menos tempo. O modelo está simplificado com apenas duas tabelas.
- Criar consumer da fila que insere no banco de dados - Consumi mais tempo do que o previsto. Não tenho muita experiência com RabbitMQ, então fiz o básico para funcionar.


## Stack de desenvolvimento

- Docker version 27.4.1, build b9d17ea
- Dotnet version 8.0.110
- Ubuntu 24.04.1 LTS
- NVIM v0.10.2
- VS Code 1.96.2
- git version 2.43.0
- beekeeper
- insomnia



## Referencias

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
