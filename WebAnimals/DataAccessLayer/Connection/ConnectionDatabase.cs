using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAnimals.DataAccessLayer.Connection
{
    public class ConnectionDatabase
    {
        private SqlConnection sqlConnection;

        public SqlConnection SqlConnection { get => sqlConnection; }

        public ConnectionDatabase()
        {
            try
            {
                string connectionString = "Data Source=200.234.224.123,54321;" +
                                          "Initial Catalog=TarikAnimales;" +
                                          "User ID=sa;" +
                                          "Password=Sql#123456789;";

                this.sqlConnection = new SqlConnection(connectionString);
            }
            catch (SqlException ex)
            {
                // Manejar cualquier otra excepción
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }
    }
}