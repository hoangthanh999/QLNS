using Quanlynhansu.Models;
using System;
using System.Data.SqlClient;


namespace Quanlynhansu.DAL
{
    public class UserDAL
    {
        public static User Login(string username, string passwordHash)
        {
            User user = null;
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT u.*, e.FullName 
                                   FROM Users u
                                   LEFT JOIN Employees e ON u.EmployeeID = e.EmployeeID
                                   WHERE u.Username = @Username 
                                   AND u.PasswordHash = @PasswordHash 
                                   AND u.IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User
                                {
                                    UserID = Convert.ToInt32(reader["UserID"]),
                                    Username = reader["Username"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    EmployeeID = reader["EmployeeID"] != DBNull.Value ? Convert.ToInt64(reader["EmployeeID"]) : (long?)null,
                                    FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : ""
                                };

                                // Cập nhật last login
                                UpdateLastLogin(user.UserID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đăng nhập: " + ex.Message);
            }
            return user;
        }

        private static void UpdateLastLogin(int userID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "UPDATE Users SET LastLogin = GETDATE() WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }
    }
}
