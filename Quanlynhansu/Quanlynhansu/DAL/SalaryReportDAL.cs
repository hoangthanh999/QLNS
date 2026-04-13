using System;
using System.Data;
using System.Data.SqlClient;
using Quanlynhansu.Models;

namespace Quanlynhansu.DAL
{
    public class SalaryReportDAL
    {
        // ========== BÁO CÁO TỔNG HỢP ==========
        public static DataTable GetSalarySummaryReport(int month, int year)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetSalarySummaryReport", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy báo cáo tổng hợp: " + ex.Message);
            }
        }

        // ========== BÁO CÁO THEO PHÒNG BAN ==========
        public static DataTable GetSalaryByDepartmentReport(int month, int year)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetSalaryByDepartmentReport", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy báo cáo phòng ban: " + ex.Message);
            }
        }

        // ========== BÁO CÁO THEO NHÂN VIÊN ==========
        public static DataTable GetSalaryByEmployeeReport(long employeeId, int fromMonth, int fromYear, int toMonth, int toYear)
        {
            try
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

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy báo cáo nhân viên: " + ex.Message);
            }
        }

        // ========== DỮ LIỆU BIỂU ĐỒ ==========
        public static DataTable GetSalaryChartData(int year)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetSalaryChartData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Year", year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy dữ liệu biểu đồ: " + ex.Message);
            }
        }

        // ========== THỐNG KÊ TỔNG QUAN ==========
        public static SalaryStatistics GetSalaryStatistics(int month, int year)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            COUNT(DISTINCT EmployeeID) AS TotalEmployees,
                            SUM(TotalSalary) AS TotalSalaryAmount,
                            AVG(TotalSalary) AS AvgSalary,
                            MAX(TotalSalary) AS MaxSalary,
                            MIN(TotalSalary) AS MinSalary,
                            SUM(Bonus) AS TotalBonus,
                            SUM(Deduction) AS TotalDeduction
                        FROM Salaries
                        WHERE Month = @Month AND Year = @Year";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new SalaryStatistics
                                {
                                    TotalEmployees = reader["TotalEmployees"] != DBNull.Value ? Convert.ToInt32(reader["TotalEmployees"]) : 0,
                                    TotalSalaryAmount = reader["TotalSalaryAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSalaryAmount"]) : 0,
                                    AvgSalary = reader["AvgSalary"] != DBNull.Value ? Convert.ToDecimal(reader["AvgSalary"]) : 0,
                                    MaxSalary = reader["MaxSalary"] != DBNull.Value ? Convert.ToDecimal(reader["MaxSalary"]) : 0,
                                    MinSalary = reader["MinSalary"] != DBNull.Value ? Convert.ToDecimal(reader["MinSalary"]) : 0,
                                    TotalBonus = reader["TotalBonus"] != DBNull.Value ? Convert.ToDecimal(reader["TotalBonus"]) : 0,
                                    TotalDeduction = reader["TotalDeduction"] != DBNull.Value ? Convert.ToDecimal(reader["TotalDeduction"]) : 0
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thống kê: " + ex.Message);
            }
            return new SalaryStatistics();
        }

        // ========== XUẤT EXCEL ==========
        public static DataTable GetSalaryReportForExport(int month, int year, string reportType)
        {
            try
            {
                switch (reportType.ToLower())
                {
                    case "summary":
                        return GetSalarySummaryReport(month, year);
                    case "department":
                        return GetSalaryByDepartmentReport(month, year);
                    default:
                        throw new Exception("Loại báo cáo không hợp lệ!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xuất báo cáo: " + ex.Message);
            }
        }

        // ========== SO SÁNH THEO THÁNG ==========
        public static DataTable CompareSalaryByMonth(int year)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            Month,
                            COUNT(DISTINCT EmployeeID) AS EmployeeCount,
                            SUM(TotalSalary) AS TotalSalary,
                            AVG(TotalSalary) AS AvgSalary,
                            SUM(Bonus) AS TotalBonus,
                            SUM(Deduction) AS TotalDeduction
                        FROM Salaries
                        WHERE Year = @Year
                        GROUP BY Month
                        ORDER BY Month";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Year", year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi so sánh lương: " + ex.Message);
            }
        }

        // ========== TOP NHÂN VIÊN LƯƠNG CAO ==========
        public static DataTable GetTopSalaryEmployees(int month, int year, int topCount = 10)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT TOP (@TopCount)
                            e.EmployeeCode,
                            e.FullName,
                            d.DepartmentName,
                            p.PositionName,
                            s.TotalSalary
                        FROM Salaries s
                        INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID
                        LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                        LEFT JOIN Positions p ON e.PositionID = p.PositionID
                        WHERE s.Month = @Month AND s.Year = @Year
                        ORDER BY s.TotalSalary DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@TopCount", topCount);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy top nhân viên: " + ex.Message);
            }
        }
    }
}
