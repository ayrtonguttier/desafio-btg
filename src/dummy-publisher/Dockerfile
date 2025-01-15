FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .

ENV RABBITMQ_HOST=queue
ENV RABBITMQ_USERNAME=superuser
ENV RABBITMQ_PASSWORD=supersenha
ENV RABBITMQ_QUEUE=novopedido

ENTRYPOINT [ "dotnet", "DummyPublisher.dll" ]
