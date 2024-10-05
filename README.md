# Cadastro de Pedidos

Este é um projeto que integra um backend em .NET com um frontend em Angular, ambos executados em contêineres Docker. O projeto permite o gerenciamento de pedidos, utilizando SQL Server como banco de dados.

## Pré-requisitos

Antes de começar, verifique se você tem as seguintes ferramentas instaladas:

- [Docker](https://www.docker.com/get-started) (inclui Docker Compose)
- [Node.js](https://nodejs.org/) (para rodar o Angular localmente, se necessário)
- [.NET SDK](https://dotnet.microsoft.com/download) (para desenvolvimento e testes no backend)

## Como Rodar o Aplicativo

### 1. Construir e iniciar os contêineres

Na raiz do projeto, execute o seguinte comando:

```bash
docker-compose up --build
```

Este comando fará o seguinte:

- **Construir as imagens Docker** para o backend e o frontend com base nos respectivos `Dockerfiles`.
- **Criar e iniciar os contêineres** para o SQL Server, backend .NET e frontend Angular.
- **Associar as portas** configuradas, permitindo o acesso aos serviços.

### 2. Acessar o Aplicativo

Após os contêineres estarem em execução, você pode acessar os seguintes serviços:

- **Frontend Angular**: Abra seu navegador e acesse [http://localhost:4200](http://localhost:4200) para visualizar a aplicação Angular.
  
- **Backend .NET e Swagger**: Acesse a API .NET e a documentação do Swagger em [http://localhost:5000](http://localhost:5000). O Swagger permitirá que você teste os endpoints da API diretamente no navegador.

## Estrutura de Dados

### 1. Banco de Dados

O banco de dados SQL Server é inicializado com as credenciais padrão definidas no `docker-compose.yml`:

- **Usuário**: `sa`
- **Senha**: `SenhaSegura123!`

### 2. API

A API fornece endpoints para gerenciar pedidos. Use a interface do Swagger para visualizar e testar os endpoints disponíveis.

## Comandos Úteis

- Para parar e remover os contêineres, execute:

  ```bash
  docker-compose down
  ```

- Para visualizar os logs dos contêineres, utilize:

  ```bash
  docker-compose logs
  ```