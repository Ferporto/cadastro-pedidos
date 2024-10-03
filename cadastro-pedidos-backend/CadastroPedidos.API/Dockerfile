# Use a imagem base do .NET SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia todos os arquivos de projeto (csproj) para seus respectivos diretórios
COPY ./CadastroPedidos.API/CadastroPedidos.API.csproj ./CadastroPedidos.API/
COPY ../CadastroPedidos.Application/CadastroPedidos.Application.csproj ./CadastroPedidos.Application/
COPY ../CadastroPedidos.Infrastructure/CadastroPedidos.Infrastructure.csproj ./CadastroPedidos.Infrastructure/
COPY ../CadastroPedidos.Domain/CadastroPedidos.Domain.csproj ./CadastroPedidos.Domain/
COPY ../CadastroPedidos.Tests/CadastroPedidos.Tests.csproj ./CadastroPedidos.Tests/

# Restaura as dependências
RUN dotnet restore ./CadastroPedidos.API/CadastroPedidos.API.csproj

# Copia todos os arquivos de código para o contêiner
COPY .. .

# Publica o aplicativo
RUN dotnet publish ./CadastroPedidos.API/CadastroPedidos.API.csproj -c Release -o out

# Cria a imagem final usando apenas o runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Define a porta para expor a API
EXPOSE 80

# Comando para rodar a aplicação .NET
ENTRYPOINT ["dotnet", "CadastroPedidos.API.dll"]
