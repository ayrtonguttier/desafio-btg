using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using PedidoApi.Database;
using PedidoApi.Database.Postgres;
using PedidoApi.Pedidos.Queries;
using PedidoApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDbConnectionFactory, PostgresConnectionFactory>();
builder.Services.AddScoped<IValorTotalDoPedidoQuery, PedidoRepository>();
builder.Services.AddScoped<IPedidosPorClienteQuery, PedidoRepository>();
builder.Services.AddScoped<IQuantidadeDePedidosPorClienteQuery, PedidoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/pedido/{codigoPedido}/total", async ([FromServices] IValorTotalDoPedidoQuery query, [FromRoute] int codigoPedido) =>
{
    var result = await query.GetValorTotalPedidoAsync(codigoPedido);
    return result.Match(item => Results.Ok(item), errors => Results.Problem(MapErrors(errors)));
})
.WithName("GetValorTotalDoPedido")
.WithOpenApi();

app.MapGet("/cliente/{codigoCliente}/pedidos", async ([FromServices] IPedidosPorClienteQuery query, int codigoCliente) =>
{
    var result = await query.GetPedidosPorClienteAsync(codigoCliente);
    return result.Match(item => Results.Ok(item), errors => Results.Problem(MapErrors(errors)));
}).WithName("GetPedidosPorCliente")
.WithOpenApi();


app.MapGet("/cliente/{codigoCliente}/pedidos/quantidade", async ([FromServices] IQuantidadeDePedidosPorClienteQuery query, int codigoCliente) =>
{
    var result = await query.GetQuantidadeDePedidosPorClienteAsync(codigoCliente);
    return result.Match(item => Results.Ok(item), errors => Results.Problem(MapErrors(errors)));
})
.WithName("Quantidade de pedidos por cliente")
.WithOpenApi();


ProblemDetails MapErrors(List<Error> errors)
{
    var result = new ProblemDetails();
    var error = errors.Single();
    result.Title = error.Code;
    result.Detail = error.Description;
    result.Status = error.Type switch
    {
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError,
    };

    return result;
}

app.Run();