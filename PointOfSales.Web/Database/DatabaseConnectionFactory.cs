using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace PointOfSales.Web.Database
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetDbConnection(DatabaseType databaseType = DatabaseType.Mysql);
    }

    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetDbConnection(DatabaseType databaseType = DatabaseType.Mysql)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (databaseType == DatabaseType.Mysql)
                return new MySqlConnection(connectionString);
            else throw new Exception("Database provider invalid.");
        }
    }

    public enum DatabaseType
    {
        Mysql,
        Mssql
    }
}
