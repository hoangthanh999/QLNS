using System;
using System.Data;
using System.Data.SqlClient;
using Quanlynhansu.Models;

namespace Quanlynhansu.DAL
{
    public class LeaveRequestDAL
    {
        // Lấy tất cả đơn nghỉ phép
        public static DataTable GetAllLeaveRequests()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        lr.LeaveRequestID,
                                        lr.EmployeeID,
                                        e.EmployeeCode,
                                        e.FullName AS EmployeeName,
                                        d.DepartmentName,
                                        p.PositionName,
                                        lr.LeaveType,
                                        lr.FromDate,
                                        lr.ToDate,
                                        lr.TotalDays,
                                        lr.Reason,
                                        lr.Status,
                                        lr.ApprovedBy,
                                        approver.FullName AS ApprovedByName,
                                        lr.ApprovedDate,
                                        lr.CreatedDate
                                    FROM LeaveRequests lr
                                    INNER JOIN Employees e ON lr.EmployeeID = e.EmployeeID
                                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                                    LEFT JOIN Positions p ON e.PositionID = p.PositionID
                                    LEFT JOIN Employees approver ON lr.ApprovedBy = approver.EmployeeID
                                    WHERE e.IsActive = 1
                                    ORDER BY lr.CreatedDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách nghỉ phép: " + ex.Message);
            }
            return dt;
        }

        // Tìm kiếm đơn nghỉ phép
        public static DataTable SearchLeaveRequests(string searchText = null, string status = null,
            DateTime? fromDate = null, DateTime? toDate = null, string leaveType = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        lr.LeaveRequestID,
                                        lr.EmployeeID,
                                        e.EmployeeCode,
                                        e.FullName AS EmployeeName,
                                        d.DepartmentName,
                                        p.PositionName,
                                        lr.LeaveType,
                                        lr.FromDate,
                                        lr.ToDate,
                                        lr.TotalDays,
                                        lr.Reason,
                                        lr.Status,
                                        lr.ApprovedBy,
                                        approver.FullName AS ApprovedByName,
                                        lr.ApprovedDate,
                                        lr.CreatedDate
                                    FROM LeaveRequests lr
                                    INNER JOIN Employees e ON lr.EmployeeID = e.EmployeeID
                                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                                    LEFT JOIN Positions p ON e.PositionID = p.PositionID
                                    LEFT JOIN Employees approver ON lr.ApprovedBy = approver.EmployeeID
                                    WHERE e.IsActive = 1";

                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        query += " AND (e.EmployeeCode LIKE @SearchText OR e.FullName LIKE @SearchText)";
                    }

                    if (!string.IsNullOrWhiteSpace(status))
                    {
                        query += " AND lr.Status = @Status";
                    }

                    if (fromDate.HasValue)
                    {
                        query += " AND lr.FromDate >= @FromDate";
                    }

                    if (toDate.HasValue)
                    {
                        query += " AND lr.ToDate <= @ToDate";
                    }

                    if (!string.IsNullOrWhiteSpace(leaveType))
                    {
                        query += " AND lr.LeaveType = @LeaveType";
                    }

                    query += " ORDER BY lr.CreatedDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(searchText))
                        {
                            cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                        }
                        if (!string.IsNullOrWhiteSpace(status))
                        {
                            cmd.Parameters.AddWithValue("@Status", status);
                        }
                        if (fromDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                        }
                        if (toDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                        }
                        if (!string.IsNullOrWhiteSpace(leaveType))
                        {
                            cmd.Parameters.AddWithValue("@LeaveType", leaveType);
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm nghỉ phép: " + ex.Message);
            }
            return dt;
        }

        // Lấy đơn nghỉ phép theo nhân viên
        public static DataTable GetLeaveRequestsByEmployee(long employeeID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        lr.LeaveRequestID,
                                        lr.LeaveType,
                                        lr.FromDate,
                                        lr.ToDate,
                                        lr.TotalDays,
                                        lr.Reason,
                                        lr.Status,
                                        approver.FullName AS ApprovedByName,
                                        lr.ApprovedDate,
                                        lr.CreatedDate
                                    FROM LeaveRequests lr
                                    LEFT JOIN Employees approver ON lr.ApprovedBy = approver.EmployeeID
                                    WHERE lr.EmployeeID = @EmployeeID
                                    ORDER BY lr.CreatedDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy lịch sử nghỉ phép: " + ex.Message);
            }
            return dt;
        }

        // Kiểm tra trùng lịch nghỉ
        public static bool CheckOverlappingLeave(long employeeID, DateTime fromDate, DateTime toDate, long? excludeId = null)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT COUNT(*) 
                                    FROM LeaveRequests 
                                    WHERE EmployeeID = @EmployeeID 
                                    AND Status IN (N'Chờ duyệt', N'Đã duyệt')
                                    AND (
                                        (@FromDate BETWEEN FromDate AND ToDate) OR
                                        (@ToDate BETWEEN FromDate AND ToDate) OR
                                        (FromDate BETWEEN @FromDate AND @ToDate)
                                    )";

                    if (excludeId.HasValue)
                    {
                        query += " AND LeaveRequestID != @ExcludeID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);

                        if (excludeId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@ExcludeID", excludeId.Value);
                        }

                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra trùng lịch: " + ex.Message);
            }
        }

        // Thêm đơn nghỉ phép
        public static bool AddLeaveRequest(LeaveRequest leave)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"INSERT INTO LeaveRequests 
                                    (EmployeeID, LeaveType, FromDate, ToDate, TotalDays, Reason, Status, CreatedDate)
                                    VALUES 
                                    (@EmployeeID, @LeaveType, @FromDate, @ToDate, @TotalDays, @Reason, @Status, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", leave.EmployeeID);
                        cmd.Parameters.AddWithValue("@LeaveType", leave.LeaveType);
                        cmd.Parameters.AddWithValue("@FromDate", leave.FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", leave.ToDate);
                        cmd.Parameters.AddWithValue("@TotalDays", leave.TotalDays);
                        cmd.Parameters.AddWithValue("@Reason", leave.Reason ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", leave.Status);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm đơn nghỉ phép: " + ex.Message);
            }
        }

        // Cập nhật đơn nghỉ phép
        public static bool UpdateLeaveRequest(LeaveRequest leave)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"UPDATE LeaveRequests SET 
                                    EmployeeID = @EmployeeID,
                                    LeaveType = @LeaveType,
                                    FromDate = @FromDate,
                                    ToDate = @ToDate,
                                    TotalDays = @TotalDays,
                                    Reason = @Reason
                                    WHERE LeaveRequestID = @LeaveRequestID 
                                    AND Status = N'Chờ duyệt'"; // Chỉ cho phép sửa đơn chưa duyệt

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LeaveRequestID", leave.LeaveRequestID);
                        cmd.Parameters.AddWithValue("@EmployeeID", leave.EmployeeID);
                        cmd.Parameters.AddWithValue("@LeaveType", leave.LeaveType);
                        cmd.Parameters.AddWithValue("@FromDate", leave.FromDate);
                        cmd.Parameters.AddWithValue("@ToDate", leave.ToDate);
                        cmd.Parameters.AddWithValue("@TotalDays", leave.TotalDays);
                        cmd.Parameters.AddWithValue("@Reason", leave.Reason ?? (object)DBNull.Value);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("Không thể sửa đơn đã được duyệt hoặc từ chối!");
                        }

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Duyệt/Từ chối đơn nghỉ phép
        public static bool ApproveLeaveRequest(long leaveRequestID, long approvedBy, string status)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"UPDATE LeaveRequests SET 
                                    Status = @Status,
                                    ApprovedBy = @ApprovedBy,
                                    ApprovedDate = GETDATE()
                                    WHERE LeaveRequestID = @LeaveRequestID 
                                    AND Status = N'Chờ duyệt'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LeaveRequestID", leaveRequestID);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@ApprovedBy", approvedBy);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("Đơn nghỉ phép đã được xử lý trước đó!");
                        }

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Hủy đơn nghỉ phép
        public static bool CancelLeaveRequest(long leaveRequestID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"UPDATE LeaveRequests SET 
                                    Status = N'Đã hủy'
                                    WHERE LeaveRequestID = @LeaveRequestID 
                                    AND Status = N'Chờ duyệt'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LeaveRequestID", leaveRequestID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("Chỉ có thể hủy đơn đang chờ duyệt!");
                        }

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Xóa đơn nghỉ phép (chỉ xóa đơn chờ duyệt)
        public static bool DeleteLeaveRequest(long leaveRequestID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"DELETE FROM LeaveRequests 
                                    WHERE LeaveRequestID = @LeaveRequestID 
                                    AND Status = N'Chờ duyệt'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LeaveRequestID", leaveRequestID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("Chỉ có thể xóa đơn đang chờ duyệt!");
                        }

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Lấy thông tin chi tiết đơn nghỉ phép
        public static LeaveRequest GetLeaveRequestById(long leaveRequestID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        lr.*,
                                        e.EmployeeCode,
                                        e.FullName AS EmployeeName,
                                        d.DepartmentName,
                                        p.PositionName,
                                        approver.FullName AS ApprovedByName
                                    FROM LeaveRequests lr
                                    INNER JOIN Employees e ON lr.EmployeeID = e.EmployeeID
                                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                                    LEFT JOIN Positions p ON e.PositionID = p.PositionID
                                    LEFT JOIN Employees approver ON lr.ApprovedBy = approver.EmployeeID
                                    WHERE lr.LeaveRequestID = @LeaveRequestID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@LeaveRequestID", leaveRequestID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new LeaveRequest
                                {
                                    LeaveRequestID = Convert.ToInt64(reader["LeaveRequestID"]),
                                    EmployeeID = Convert.ToInt64(reader["EmployeeID"]),
                                    EmployeeCode = reader["EmployeeCode"].ToString(),
                                    EmployeeName = reader["EmployeeName"].ToString(),
                                    DepartmentName = reader["DepartmentName"]?.ToString(),
                                    PositionName = reader["PositionName"]?.ToString(),
                                    LeaveType = reader["LeaveType"].ToString(),
                                    FromDate = Convert.ToDateTime(reader["FromDate"]),
                                    ToDate = Convert.ToDateTime(reader["ToDate"]),
                                    TotalDays = Convert.ToInt32(reader["TotalDays"]),
                                    Reason = reader["Reason"]?.ToString(),
                                    Status = reader["Status"].ToString(),
                                    ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? Convert.ToInt64(reader["ApprovedBy"]) : (long?)null,
                                    ApprovedByName = reader["ApprovedByName"]?.ToString(),
                                    ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin đơn nghỉ phép: " + ex.Message);
            }
            return null;
        }

        // Lấy danh sách nhân viên (cho ComboBox)
        public static DataTable GetActiveEmployees()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        EmployeeID, 
                                        EmployeeCode + ' - ' + FullName AS DisplayName,
                                        EmployeeCode,
                                        FullName
                                    FROM Employees 
                                    WHERE IsActive = 1 AND Status = N'Đang làm việc'
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
        // Thêm vào class LeaveRequestDAL

        public static DataTable GetApprovedLeaveDays(long employeeId, int month, int year)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = @"
            SELECT 
                ISNULL(SUM(TotalDays), 0) AS LeaveDays
            FROM LeaveRequests
            WHERE EmployeeID = @EmployeeID
              AND Status = N'Đã duyệt'
              AND (
                  (YEAR(FromDate) = @Year AND MONTH(FromDate) = @Month)
                  OR (YEAR(ToDate) = @Year AND MONTH(ToDate) = @Month)
              )";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Year", year);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Thống kê nghỉ phép theo nhân viên
        public static DataTable GetLeaveStatistics(int year)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"SELECT 
                                        e.EmployeeCode,
                                        e.FullName,
                                        d.DepartmentName,
                                        COUNT(lr.LeaveRequestID) AS TotalRequests,
                                        SUM(CASE WHEN lr.Status = N'Đã duyệt' THEN lr.TotalDays ELSE 0 END) AS ApprovedDays,
                                        SUM(CASE WHEN lr.Status = N'Chờ duyệt' THEN lr.TotalDays ELSE 0 END) AS PendingDays
                                    FROM Employees e
                                    LEFT JOIN LeaveRequests lr ON e.EmployeeID = lr.EmployeeID 
                                        AND YEAR(lr.FromDate) = @Year
                                    LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                                    WHERE e.IsActive = 1
                                    GROUP BY e.EmployeeCode, e.FullName, d.DepartmentName
                                    ORDER BY e.EmployeeCode";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Year", year);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thống kê nghỉ phép: " + ex.Message);
            }
            return dt;
        }
    }
}
