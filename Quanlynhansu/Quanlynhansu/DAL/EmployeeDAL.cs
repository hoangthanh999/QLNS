using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Quanlynhansu.Models;

namespace Quanlynhansu.DAL
{
    public class EmployeeDAL
    {
        // ========== TÌM KIẾM VỚI PHÂN TRANG ==========
        public static DataTable SearchEmployees(string searchText, int? departmentID, string status, int pageNumber, int pageSize)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SearchEmployees", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@SearchText", (object)searchText ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DepartmentID", (object)departmentID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        conn.Open();
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm nhân viên: " + ex.Message);
            }
            return dt;
        }

        // ========== LẤY THÔNG TIN NHÂN VIÊN THEO ID (OBJECT) ==========
        public static Employee GetEmployeeByID(long employeeID)
        {
            Employee emp = null;
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            e.*, 
                            d.DepartmentName, 
                            p.PositionName 
                        FROM Employees e
                        LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                        LEFT JOIN Positions p ON e.PositionID = p.PositionID
                        WHERE e.EmployeeID = @EmployeeID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                emp = new Employee
                                {
                                    EmployeeID = Convert.ToInt64(reader["EmployeeID"]),
                                    EmployeeCode = reader["EmployeeCode"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]) : (DateTime?)null,
                                    Gender = reader["Gender"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    IdentityCard = reader["IdentityCard"].ToString(),
                                    DepartmentID = reader["DepartmentID"] != DBNull.Value ? Convert.ToInt32(reader["DepartmentID"]) : (int?)null,
                                    DepartmentName = reader["DepartmentName"].ToString(),
                                    PositionID = reader["PositionID"] != DBNull.Value ? Convert.ToInt32(reader["PositionID"]) : (int?)null,
                                    PositionName = reader["PositionName"].ToString(),
                                    HireDate = Convert.ToDateTime(reader["HireDate"]),
                                    Status = reader["Status"].ToString(),
                                    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : (decimal?)null
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin nhân viên: " + ex.Message);
            }
            return emp;
        }

        // ========== LẤY TẤT CẢ NHÂN VIÊN (CHO COMBOBOX) ==========
        public static DataTable GetAllEmployees()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            e.EmployeeID,
                            e.EmployeeCode,
                            e.FullName,
                            e.Email,
                            e.PhoneNumber,
                            e.Salary,
                            ISNULL(d.DepartmentName, N'Chưa phân') AS DepartmentName,
                            ISNULL(p.PositionName, N'Chưa có') AS PositionName,
                            e.Status
                        FROM Employees e
                        LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                        LEFT JOIN Positions p ON e.PositionID = p.PositionID
                        WHERE e.IsActive = 1
                        ORDER BY e.EmployeeCode";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách nhân viên: " + ex.Message);
            }
        }

        // ========== LẤY THÔNG TIN NHÂN VIÊN THEO ID (DATATABLE) ==========
        public static DataTable GetEmployeeById(long employeeId)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            e.*,
                            ISNULL(d.DepartmentName, N'Chưa phân') AS DepartmentName,
                            ISNULL(p.PositionName, N'Chưa có') AS PositionName
                        FROM Employees e
                        LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                        LEFT JOIN Positions p ON e.PositionID = p.PositionID
                        WHERE e.EmployeeID = @EmployeeID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin nhân viên: " + ex.Message);
            }
        }
        public static DataTable GetActiveEmployeesForComboBox()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            EmployeeID,
                            EmployeeCode,
                            FullName,
                            EmployeeCode + ' - ' + FullName AS DisplayName
                        FROM Employees
                        WHERE IsActive = 1 
                          AND Status = N'Đang làm việc'
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

        // ========== THÊM NHÂN VIÊN MỚI ==========
        public static bool InsertEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        INSERT INTO Employees 
                        (EmployeeCode, FullName, DateOfBirth, Gender, PhoneNumber, Email, 
                         Address, IdentityCard, DepartmentID, PositionID, HireDate, Status, Salary)
                        VALUES 
                        (@EmployeeCode, @FullName, @DateOfBirth, @Gender, @PhoneNumber, @Email, 
                         @Address, @IdentityCard, @DepartmentID, @PositionID, @HireDate, @Status, @Salary)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
                        cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", (object)emp.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", (object)emp.Gender ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNumber", (object)emp.PhoneNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", (object)emp.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", (object)emp.Address ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IdentityCard", (object)emp.IdentityCard ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DepartmentID", (object)emp.DepartmentID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PositionID", (object)emp.PositionID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@HireDate", emp.HireDate);
                        cmd.Parameters.AddWithValue("@Status", emp.Status);
                        cmd.Parameters.AddWithValue("@Salary", (object)emp.Salary ?? DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm nhân viên: " + ex.Message);
            }
        }

        // ========== CẬP NHẬT NHÂN VIÊN ==========
        public static bool UpdateEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        UPDATE Employees SET 
                            FullName = @FullName,
                            DateOfBirth = @DateOfBirth,
                            Gender = @Gender,
                            PhoneNumber = @PhoneNumber,
                            Email = @Email,
                            Address = @Address,
                            IdentityCard = @IdentityCard,
                            DepartmentID = @DepartmentID,
                            PositionID = @PositionID,
                            Status = @Status,
                            Salary = @Salary,
                            ModifiedDate = GETDATE()
                        WHERE EmployeeID = @EmployeeID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
                        cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", (object)emp.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", (object)emp.Gender ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNumber", (object)emp.PhoneNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", (object)emp.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", (object)emp.Address ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IdentityCard", (object)emp.IdentityCard ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DepartmentID", (object)emp.DepartmentID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PositionID", (object)emp.PositionID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", emp.Status);
                        cmd.Parameters.AddWithValue("@Salary", (object)emp.Salary ?? DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật nhân viên: " + ex.Message);
            }
        }

        // ========== XÓA NHÂN VIÊN (SOFT DELETE) ==========
        public static bool DeleteEmployee(long employeeID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "UPDATE Employees SET IsActive = 0 WHERE EmployeeID = @EmployeeID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa nhân viên: " + ex.Message);
            }
        }

        // ========== ĐẾM TỔNG SỐ NHÂN VIÊN ==========
        public static long GetTotalEmployees(string searchText, int? departmentID, string status)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT COUNT(*) 
                        FROM Employees e
                        WHERE (@SearchText IS NULL OR 
                               e.FullName LIKE '%' + @SearchText + '%' OR 
                               e.EmployeeCode LIKE '%' + @SearchText + '%')
                        AND (@DepartmentID IS NULL OR e.DepartmentID = @DepartmentID)
                        AND (@Status IS NULL OR e.Status = @Status)
                        AND e.IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SearchText", (object)searchText ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DepartmentID", (object)departmentID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);

                        conn.Open();
                        return Convert.ToInt64(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đếm nhân viên: " + ex.Message);
            }
        }
        // ========== KIỂM TRA MÃ NHÂN VIÊN ĐÃ TỒN TẠI ==========
        public static bool IsEmployeeCodeExists(string employeeCode)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "SELECT COUNT(*) FROM Employees WHERE EmployeeCode = @EmployeeCode AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra mã nhân viên: " + ex.Message);
            }
        }

        // ========== IMPORT NHIỀU NHÂN VIÊN (BULK INSERT) ==========
        public static int BulkInsertEmployees(List<Employee> employees)
        {
            int successCount = 0;
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string query = @"
                        INSERT INTO Employees 
                        (EmployeeCode, FullName, DateOfBirth, Gender, PhoneNumber, Email, 
                         Address, IdentityCard, DepartmentID, PositionID, HireDate, Status, Salary)
                        VALUES 
                        (@EmployeeCode, @FullName, @DateOfBirth, @Gender, @PhoneNumber, @Email, 
                         @Address, @IdentityCard, @DepartmentID, @PositionID, @HireDate, @Status, @Salary)";

                            foreach (Employee emp in employees)
                            {
                                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
                                    cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                                    cmd.Parameters.AddWithValue("@DateOfBirth", (object)emp.DateOfBirth ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Gender", (object)emp.Gender ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@PhoneNumber", (object)emp.PhoneNumber ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Email", (object)emp.Email ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Address", (object)emp.Address ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@IdentityCard", (object)emp.IdentityCard ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@DepartmentID", (object)emp.DepartmentID ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@PositionID", (object)emp.PositionID ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@HireDate", emp.HireDate);
                                    cmd.Parameters.AddWithValue("@Status", emp.Status);
                                    cmd.Parameters.AddWithValue("@Salary", (object)emp.Salary ?? DBNull.Value);

                                    if (cmd.ExecuteNonQuery() > 0)
                                        successCount++;
                                }
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi import nhân viên: " + ex.Message);
            }
            return successCount;
        }

    }
}
