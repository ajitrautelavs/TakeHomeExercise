using System.Data;
using System.Data.SqlClient;
using System.Text;
using API.Core.Services;
using Microsoft.Extensions.Configuration;
using static API.Core.Classes.Enums;

namespace API.Core.Data
{
    public class DbManager : IDbManager
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DbManager(EnumDB Name, DbAccessLevel AccessLevel, IConfiguration config)
        {
            _configuration = config;
            var sb = new StringBuilder();
            sb.Append(Name.ToString());

            sb.Append(AccessLevel.ToString());
            // Instead of below If statment use above ToString()
            //if (AccessLevel == DbAccessLevel.WRITE)
            //{
            //    sb.Append("Write");
            //}
            //else
            //{
            //    sb.Append("Read");
            //}
            _connectionString = _configuration.GetConnectionString(sb.ToString()).ToString();
        }

        public IDbConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            return conn;
        }

        public IDbConnection GetOpenConnection()
        {
            var conn = GetConnection();
            conn.Open();
            return conn;
        }
    }
}
