using CadastroPedidos.Application.Services;
using CadastroPedidos.Domain.Utils.Dependencies;
using CadastroPedidos.Domain.Utils.Repositories;
using CadastroPedidos.Domain.Utils.UnitOfWork;
using CadastroPedidos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com SQL Server
builder.Services.AddDbContext<CadastroPedidosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar controllers
builder.Services.AddControllers();

// Configuração automática de dependências
// Adiciona o DependencyConfigurator para registrar serviços automaticamente
//var configurator = new DependencyConfigurator(Assembly.GetExecutingAssembly());
//configurator.ConfigureDependencies(builder.Services);
builder.Services.AddScoped<IDbContextWithTransactions, CadastroPedidosDbContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IPedidoService, PedidoService>();
builder.Services.AddTransient<IItensPedidoService, ItensPedidoService>();
builder.Services.AddTransient<IProdutoService, ProdutoService>();

var app = builder.Build();

// Chamar o método para aplicar as migrações ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CadastroPedidosDbContext>();
    // Aplica as migrações automaticamente ao iniciar a aplicação
    dbContext.Database.Migrate();
}

// Configurar Swagger para o ambiente de desenvolvimento
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadastro de Pedidos API v1");
        c.RoutePrefix = string.Empty;  // Define Swagger como a página inicial do backend
    });
//}

app.UseRouting();

// Mapear os controllers
app.MapControllers();

app.Run();
