using Dapper;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EvaluationSystem.Persistence
{
    public static class CreateDatabase
    {
        public static void EnsureDatabase(IConfiguration configuration)
        {
            var masterConnectionString = configuration.GetConnectionString("MasterConnection");
            var evaluationConnectionString = new SqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));

            var name = evaluationConnectionString.InitialCatalog;

            var parameters = new DynamicParameters();
            parameters.Add("name", name);
            using var connection = new SqlConnection(masterConnectionString);
            var records = connection.Query("SELECT * FROM sys.databases WHERE name = @name",
                 parameters);
            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {name}");
            }
        }
    }
}
