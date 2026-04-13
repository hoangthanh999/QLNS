using System;
using System.Data;
using System.Data.SqlClient;
using Quanlynhansu.Models;

namespace Quanlynhansu.DAL
{
    public class SalaryDAL
    {
        // ========== TÌM KIẾM & DANH SÁCH ==========

        public static DataTable SearchSalaries(int? month, int? year, string status, string searchText)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                        s.SalaryID,
                        s.EmployeeID,
                        e.EmployeeCode,
                        e.FullName AS EmployeeName,
                        d.DepartmentName,
                        p.PositionName,
                        s.Month,
                        s.Year,
                        s.BaseSalary,
                        s.Bonus,
                        s.Deduction,
                        s.TotalSalary,
                        s.PaymentDate,
                        s.Status
                    FROM Salaries s
                    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                    LEFT JOIN Positions p ON e.PositionID = p.PositionID
                    WHERE 1=1
                        AND (@Month IS NULL OR s.Month = @Month)
                        AND (@Year IS NULL OR s.Year = @Year)
                        AND (@Status IS NULL OR @Status = '' OR s.Status = @Status)
                        AND (@SearchText IS NULL OR @SearchText = '' OR 
                             e.FullName LIKE '%' + @SearchText + '%' OR 
                             e.EmployeeCode LIKE '%' + @SearchText + '%')
                    ORDER BY s.Year DESC, s.Month DESC, e.EmployeeCode";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Month", (object)month ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SearchText", (object)searchText ?? DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static Salary GetSalaryById(long salaryId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                        s.*,
                        e.EmployeeCode,
                        e.FullName AS EmployeeName,
                        d.DepartmentName,
                        p.PositionName
                    FROM Salaries s
                    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                    LEFT JOIN Positions p ON e.PositionID = p.PositionID
                    WHERE s.SalaryID = @SalaryID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SalaryID", salaryId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Salary
                    {
                        SalaryID = Convert.ToInt64(reader["SalaryID"]),
                        EmployeeID = Convert.ToInt64(reader["EmployeeID"]),
                        EmployeeCode = reader["EmployeeCode"].ToString(),
                        EmployeeName = reader["EmployeeName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        PositionName = reader["PositionName"].ToString(),
                        Month = Convert.ToInt32(reader["Month"]),
                        Year = Convert.ToInt32(reader["Year"]),
                        BaseSalary = Convert.ToDecimal(reader["BaseSalary"]),
                        Bonus = Convert.ToDecimal(reader["Bonus"]),
                        Deduction = Convert.ToDecimal(reader["Deduction"]),
                        TotalSalary = Convert.ToDecimal(reader["TotalSalary"]),
                        PaymentDate = reader["PaymentDate"] != DBNull.Value
                            ? Convert.ToDateTime(reader["PaymentDate"])
                            : (DateTime?)null,
                        Status = reader["Status"].ToString()
                    };
                }
                return null;
            }
        }

        // ========== THÊM/SỬA/XÓA ==========

        public static bool AddSalary(Salary salary)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    INSERT INTO Salaries (EmployeeID, Month, Year, BaseSalary, Bonus, Deduction, TotalSalary, Status)
                    VALUES (@EmployeeID, @Month, @Year, @BaseSalary, @Bonus, @Deduction, @TotalSalary, @Status)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", salary.EmployeeID);
                cmd.Parameters.AddWithValue("@Month", salary.Month);
                cmd.Parameters.AddWithValue("@Year", salary.Year);
                cmd.Parameters.AddWithValue("@BaseSalary", salary.BaseSalary);
                cmd.Parameters.AddWithValue("@Bonus", salary.Bonus);
                cmd.Parameters.AddWithValue("@Deduction", salary.Deduction);
                cmd.Parameters.AddWithValue("@TotalSalary", salary.TotalSalary);
                cmd.Parameters.AddWithValue("@Status", salary.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool UpdateSalary(Salary salary)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    UPDATE Salaries 
                    SET BaseSalary = @BaseSalary,
                        Bonus = @Bonus,
                        Deduction = @Deduction,
                        TotalSalary = @TotalSalary,
                        Status = @Status,
                        PaymentDate = @PaymentDate
                    WHERE SalaryID = @SalaryID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SalaryID", salary.SalaryID);
                cmd.Parameters.AddWithValue("@BaseSalary", salary.BaseSalary);
                cmd.Parameters.AddWithValue("@Bonus", salary.Bonus);
                cmd.Parameters.AddWithValue("@Deduction", salary.Deduction);
                cmd.Parameters.AddWithValue("@TotalSalary", salary.TotalSalary);
                cmd.Parameters.AddWithValue("@Status", salary.Status);
                cmd.Parameters.AddWithValue("@PaymentDate", (object)salary.PaymentDate ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteSalary(long salaryId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Salaries WHERE SalaryID = @SalaryID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SalaryID", salaryId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ========== TÍNH LƯƠNG ==========

        public static DataTable GetEmployeesForSalaryCalculation(int month, int year, long? departmentId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                        e.EmployeeID,
                        e.EmployeeCode,
                        e.FullName,
                        d.DepartmentName,
                        p.PositionName,
                        e.Salary AS BaseSalary,
                        ISNULL(att.WorkDays, 0) AS WorkDays,
                        ISNULL(att.WorkHours, 0) AS WorkHours,
                        ISNULL(lv.LeaveDays, 0) AS LeaveDays,
                        CASE 
                            WHEN EXISTS (
                                SELECT 1 FROM Salaries s 
                                WHERE s.EmployeeID = e.EmployeeID 
                                AND s.Month = @Month AND s.Year = @Year
                            ) THEN 1 ELSE 0 
                        END AS HasSalary
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                    LEFT JOIN Positions p ON e.PositionID = p.PositionID
                    LEFT JOIN (
                        SELECT 
                            EmployeeID,
                            COUNT(*) AS WorkDays,
                            SUM(WorkHours) AS WorkHours
                        FROM Attendance
                        WHERE MONTH(AttendanceDate) = @Month 
                        AND YEAR(AttendanceDate) = @Year
                        AND Status = N'Đi làm'
                        GROUP BY EmployeeID
                    ) att ON e.EmployeeID = att.EmployeeID
                    LEFT JOIN (
                        SELECT 
                            EmployeeID,
                            SUM(TotalDays) AS LeaveDays
                        FROM LeaveRequests
                        WHERE MONTH(FromDate) = @Month 
                        AND YEAR(FromDate) = @Year
                        AND Status = N'Đã duyệt'
                        GROUP BY EmployeeID
                    ) lv ON e.EmployeeID = lv.EmployeeID
                    WHERE e.IsActive = 1
                        AND e.Status = N'Đang làm việc'
                        AND (@DepartmentID IS NULL OR e.DepartmentID = @DepartmentID)
                    ORDER BY e.EmployeeCode";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@DepartmentID", (object)departmentId ?? DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static bool CheckSalaryExists(long employeeId, int month, int year)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Salaries 
                    WHERE EmployeeID = @EmployeeID 
                    AND Month = @Month 
                    AND Year = @Year";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // ========== BÁO CÁO & THỐNG KÊ ==========

        public static DataTable GetSalaryReport(int month, int year)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                        d.DepartmentName,
                        COUNT(s.SalaryID) AS EmployeeCount,
                        SUM(s.BaseSalary) AS TotalBaseSalary,
                        SUM(s.Bonus) AS TotalBonus,
                        SUM(s.Deduction) AS TotalDeduction,
                        SUM(s.TotalSalary) AS TotalSalary,
                        AVG(s.TotalSalary) AS AvgSalary
                    FROM Salaries s
                    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                    WHERE s.Month = @Month AND s.Year = @Year
                    GROUP BY d.DepartmentName
                    ORDER BY TotalSalary DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetSalaryStatistics(int year)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    SELECT 
                        s.Month,
                        COUNT(s.SalaryID) AS EmployeeCount,
                        SUM(s.TotalSalary) AS TotalSalary,
                        AVG(s.TotalSalary) AS AvgSalary,
                        MAX(s.TotalSalary) AS MaxSalary,
                        MIN(s.TotalSalary) AS MinSalary
                    FROM Salaries s
                    WHERE s.Year = @Year
                    GROUP BY s.Month
                    ORDER BY s.Month";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // ========== CẬP NHẬT TRẠNG THÁI ==========

        public static bool UpdateSalaryStatus(long salaryId, string status, DateTime? paymentDate = null)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    UPDATE Salaries 
                    SET Status = @Status,
                        PaymentDate = @PaymentDate
                    WHERE SalaryID = @SalaryID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SalaryID", salaryId);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@PaymentDate", (object)paymentDate ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        // Thêm vào class SalaryDAL

        public static DataTable GetSalarySummaryReport(int month, int year)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetSalarySummaryReport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetSalaryByDepartmentReport(int month, int year)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetSalaryByDepartmentReport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetSalaryByEmployeeReport(long employeeId, int fromMonth, int fromYear, int toMonth, int toYear)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetSalaryByEmployeeReport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@FromMonth", fromMonth);
                    cmd.Parameters.AddWithValue("@FromYear", fromYear);
                    cmd.Parameters.AddWithValue("@ToMonth", toMonth);
                    cmd.Parameters.AddWithValue("@ToYear", toYear);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetSalaryChartData(int year)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetSalaryChartData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Year", year);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static bool BulkUpdateStatus(int month, int year, string status)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
                    UPDATE Salaries 
                    SET Status = @Status
                    WHERE Month = @Month AND Year = @Year";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
