# Estágio base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Adiciona ferramentas de rede (net-tools)
RUN apt-get update && apt-get install -y net-tools

# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CadastroPedidos.API/CadastroPedidos.API.csproj", "CadastroPedidos.API/"]
COPY ["CadastroPedidos.Infrastructure/CadastroPedidos.Infrastructure.csproj", "CadastroPedidos.Infrastructure/"]
RUN dotnet restore "CadastroPedidos.API/CadastroPedidos.API.csproj"
COPY . .
WORKDIR "/src/CadastroPedidos.API"
RUN dotnet build "CadastroPedidos.API.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "CadastroPedidos.API.csproj" -c Release -o /app/publish

# Estágio final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadastroPedidos.API.dll"]
