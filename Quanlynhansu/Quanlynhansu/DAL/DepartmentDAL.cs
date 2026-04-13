using System;
using System.Data;
using System.Data.SqlClient;
using Quanlynhansu.Models;

namespace Quanlynhansu.DAL
{
    public class DepartmentDAL
    {
        public static DataTable GetAllDepartments()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        d.DepartmentID,
                                        d.DepartmentCode,
                                        d.DepartmentName,
                                        d.ManagerID,
                                        ISNULL(e.FullName, N'Chưa có') AS ManagerName,
                                        d.ParentDepartmentID,
                                        ISNULL(pd.DepartmentName, N'Không có') AS ParentDepartmentName,
                                        d.CreatedDate,
                                        d.IsActive,
                                        (SELECT COUNT(*) FROM Employees WHERE DepartmentID = d.DepartmentID AND IsActive = 1) AS EmployeeCount
                                    FROM Departments d
                                    LEFT JOIN Employees e ON d.ManagerID = e.EmployeeID
                                    LEFT JOIN Departments pd ON d.ParentDepartmentID = pd.DepartmentID
                                    WHERE d.IsActive = 1
                                    ORDER BY d.DepartmentCode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách phòng ban: " + ex.Message);
            }
            return dt;
        }

        public static bool CheckDepartmentCodeExists(string departmentCode, int? excludeId = null)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM Departments WHERE DepartmentCode = @DepartmentCode AND IsActive = 1";

                    if (excludeId.HasValue)
                    {
                        query += " AND DepartmentID != @DepartmentID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentCode", departmentCode);
                        if (excludeId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@DepartmentID", excludeId.Value);
                        }

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra mã phòng ban: " + ex.Message);
            }
        }

        public static bool AddDepartment(Department dept)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"INSERT INTO Departments 
                                    (DepartmentCode, DepartmentName, ManagerID, ParentDepartmentID, CreatedDate, IsActive)
                                    VALUES 
                                    (@DepartmentCode, @DepartmentName, @ManagerID, @ParentDepartmentID, @CreatedDate, @IsActive)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentCode", dept.DepartmentCode);
                        cmd.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
                        cmd.Parameters.AddWithValue("@ManagerID", dept.ManagerID.HasValue ? (object)dept.ManagerID.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ParentDepartmentID", dept.ParentDepartmentID.HasValue ? (object)dept.ParentDepartmentID.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IsActive", true);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm phòng ban: " + ex.Message);
            }
        }

        public static bool UpdateDepartment(Department dept)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"UPDATE Departments SET 
                                    DepartmentCode = @DepartmentCode,
                                    DepartmentName = @DepartmentName,
                                    ManagerID = @ManagerID,
                                    ParentDepartmentID = @ParentDepartmentID
                                    WHERE DepartmentID = @DepartmentID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentID", dept.DepartmentID);
                        cmd.Parameters.AddWithValue("@DepartmentCode", dept.DepartmentCode);
                        cmd.Parameters.AddWithValue("@DepartmentName", dept.DepartmentName);
                        cmd.Parameters.AddWithValue("@ManagerID", dept.ManagerID.HasValue ? (object)dept.ManagerID.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ParentDepartmentID", dept.ParentDepartmentID.HasValue ? (object)dept.ParentDepartmentID.Value : DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật phòng ban: " + ex.Message);
            }
        }

        public static bool DeleteDepartment(int departmentID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra có nhân viên không
                    string checkQuery = "SELECT COUNT(*) FROM Employees WHERE DepartmentID = @DepartmentID AND IsActive = 1";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            throw new Exception($"Không thể xóa! Phòng ban đang có {count} nhân viên.");
                        }
                    }

                    // Kiểm tra có phòng ban con không
                    string checkChildQuery = "SELECT COUNT(*) FROM Departments WHERE ParentDepartmentID = @DepartmentID AND IsActive = 1";
                    using (SqlCommand checkChildCmd = new SqlCommand(checkChildQuery, conn))
                    {
                        checkChildCmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                        int childCount = (int)checkChildCmd.ExecuteScalar();

                        if (childCount > 0)
                        {
                            throw new Exception($"Không thể xóa! Phòng ban đang có {childCount} phòng ban con.");
                        }
                    }

                    // Xóa mềm (set IsActive = 0)
                    string deleteQuery = "UPDATE Departments SET IsActive = 0 WHERE DepartmentID = @DepartmentID";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Department GetDepartmentById(int departmentID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT * FROM Departments WHERE DepartmentID = @DepartmentID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Department
                                {
                                    DepartmentID = Convert.ToInt32(reader["DepartmentID"]),
                                    DepartmentCode = reader["DepartmentCode"].ToString(),
                                    DepartmentName = reader["DepartmentName"].ToString(),
                                    ManagerID = reader["ManagerID"] != DBNull.Value ? Convert.ToInt64(reader["ManagerID"]) : (long?)null,
                                    ParentDepartmentID = reader["ParentDepartmentID"] != DBNull.Value ? Convert.ToInt32(reader["ParentDepartmentID"]) : (int?)null,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                                    IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin phòng ban: " + ex.Message);
            }
            return null;
        }

        // Lấy danh sách phòng ban cha (cho dropdown)
        public static DataTable GetParentDepartments()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT DepartmentID, DepartmentCode + ' - ' + DepartmentName AS DepartmentName
                                    FROM Departments
                                    WHERE IsActive = 1
                                    ORDER BY DepartmentCode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách phòng ban cha: " + ex.Message);
            }
            return dt;
        }

        // Lấy danh sách nhân viên (cho dropdown Manager)
        public static DataTable GetEmployeesForManager()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT EmployeeID, EmployeeCode + ' - ' + FullName AS EmployeeName
                                    FROM Employees
                                    WHERE IsActive = 1
                                    ORDER BY EmployeeCode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách nhân viên: " + ex.Message);
            }
            return dt;
        }
    }
}
