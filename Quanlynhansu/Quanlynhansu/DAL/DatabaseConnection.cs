using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Quanlynhansu.DAL
{
    public class DatabaseConnection
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["HRMConnection"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kết nối database: " + ex.Message);
            }
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
