using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace InterfVSAbstVCompDemo.DAL
{
    internal class DatabaseHandler
    {
        private readonly string _connectionString;

        public DatabaseHandler()
        {
            _connectionString = "Host=localhost; Port=5433; Username=postgres; Password=postgres; Database=postgres";
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public void EnsureTableExists()
        {
            using var conn = GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS animal (
                            id SERIAL PRIMARY KEY,
                            name VARCHAR(255),
                            species VARCHAR(50),
                            birthdate DATE,
                            element VARCHAR(100));";
            cmd.ExecuteNonQuery();
        }
    }
}
