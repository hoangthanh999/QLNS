using System;
using System.Data;
using System.Data.SqlClient;
using Quanlynhansu.Models;

namespace Quanlynhansu.DAL
{
    public class PositionDAL
    {
        public static DataTable GetAllPositions()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        p.PositionID,
                                        p.PositionCode,
                                        p.PositionName,
                                        p.Level,
                                        CASE p.Level
                                            WHEN 1 THEN N'Cấp 1 - Lãnh đạo'
                                            WHEN 2 THEN N'Cấp 2 - Quản lý'
                                            WHEN 3 THEN N'Cấp 3 - Trưởng nhóm'
                                            WHEN 4 THEN N'Cấp 4 - Nhân viên'
                                            WHEN 5 THEN N'Cấp 5 - Thực tập'
                                            ELSE N'Cấp ' + CAST(p.Level AS NVARCHAR)
                                        END AS LevelName,
                                        ISNULL(p.BaseSalary, 0) AS BaseSalary,
                                        p.IsActive,
                                        (SELECT COUNT(*) FROM Employees WHERE PositionID = p.PositionID AND IsActive = 1) AS EmployeeCount
                                    FROM Positions p
                                    WHERE p.IsActive = 1
                                    ORDER BY p.Level, p.PositionCode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách chức vụ: " + ex.Message);
            }
            return dt;
        }

        public static bool CheckPositionCodeExists(string positionCode, int? excludeId = null)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM Positions WHERE PositionCode = @PositionCode AND IsActive = 1";

                    if (excludeId.HasValue)
                    {
                        query += " AND PositionID != @PositionID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PositionCode", positionCode);
                        if (excludeId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@PositionID", excludeId.Value);
                        }

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra mã chức vụ: " + ex.Message);
            }
        }

        public static bool AddPosition(Position position)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"INSERT INTO Positions 
                                    (PositionCode, PositionName, Level, BaseSalary, IsActive)
                                    VALUES 
                                    (@PositionCode, @PositionName, @Level, @BaseSalary, @IsActive)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PositionCode", position.PositionCode);
                        cmd.Parameters.AddWithValue("@PositionName", position.PositionName);
                        cmd.Parameters.AddWithValue("@Level", position.Level);
                        cmd.Parameters.AddWithValue("@BaseSalary", position.BaseSalary.HasValue ? (object)position.BaseSalary.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsActive", true);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm chức vụ: " + ex.Message);
            }
        }

        public static bool UpdatePosition(Position position)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"UPDATE Positions SET 
                                    PositionCode = @PositionCode,
                                    PositionName = @PositionName,
                                    Level = @Level,
                                    BaseSalary = @BaseSalary
                                    WHERE PositionID = @PositionID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PositionID", position.PositionID);
                        cmd.Parameters.AddWithValue("@PositionCode", position.PositionCode);
                        cmd.Parameters.AddWithValue("@PositionName", position.PositionName);
                        cmd.Parameters.AddWithValue("@Level", position.Level);
                        cmd.Parameters.AddWithValue("@BaseSalary", position.BaseSalary.HasValue ? (object)position.BaseSalary.Value : DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật chức vụ: " + ex.Message);
            }
        }

        public static bool DeletePosition(int positionID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra có nhân viên không
                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE PositionID = @PositionID AND IsActive = 1";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@PositionID", positionID);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            throw new Exception($"Không thể xóa! Chức vụ đang có {count} nhân viên.");
                        }
                    }

                    // Xóa mềm (set IsActive = 0)
                    string deleteQuery = "UPDATE Positions SET IsActive = 0 WHERE PositionID = @PositionID";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@PositionID", positionID);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Position GetPositionById(int positionID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT * FROM Positions WHERE PositionID = @PositionID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PositionID", positionID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Position
                                {
                                    PositionID = Convert.ToInt32(reader["PositionID"]),
                                    PositionCode = reader["PositionCode"].ToString(),
                                    PositionName = reader["PositionName"].ToString(),
                                    Level = Convert.ToInt32(reader["Level"]),
                                    BaseSalary = reader["BaseSalary"] != DBNull.Value ? Convert.ToDecimal(reader["BaseSalary"]) : (decimal?)null,
                                    IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin chức vụ: " + ex.Message);
            }
            return null;
        }
    }
}
