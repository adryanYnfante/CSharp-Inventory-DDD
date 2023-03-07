using InventoryApp.Domain.Interfaces.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace InventoryApp.Infrastructure.Contexts
{
    public class InventoryDapperContext
    {
        private readonly string _connectionString;

        public InventoryDapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlConnection") ?? throw new NullReferenceException();
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}