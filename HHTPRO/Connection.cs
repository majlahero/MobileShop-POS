using System;
using System.Data.SqlClient;

namespace HTTT
{
    public class DatabaseConnection
    {
        private string connectionString;

        public DatabaseConnection()
        {
            connectionString = "Server=LAPTOP-ESFJ9H96\\SQLEXPRESS;Database=HTTT;Trusted_Connection=True;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString); 
        }
    }
}