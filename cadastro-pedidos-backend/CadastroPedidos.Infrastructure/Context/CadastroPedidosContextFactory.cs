using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CadastroPedidos.Infrastructure.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CadastroPedidosDbContext>
    {
        public CadastroPedidosDbContext CreateDbContext(string[] args)
        {
            // Construir o IConfiguration para acessar a ConnectionString
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Configurar o DbContextOptions com a ConnectionString
            var builder = new DbContextOptionsBuilder<CadastroPedidosDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new CadastroPedidosDbContext(builder.Options);
        }
    }
}
