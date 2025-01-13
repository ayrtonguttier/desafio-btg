FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENV POSTGRES_HOST=database
ENV POSTGRES_PORT=5432
ENV POSTGRES_USERNAME=usuario
ENV POSTGRES_PASSWORD=senhaforte
ENV POSTGRES_DATABASE=desafiobtg

ENTRYPOINT [ "dotnet", "PedidoApi.dll" ]
