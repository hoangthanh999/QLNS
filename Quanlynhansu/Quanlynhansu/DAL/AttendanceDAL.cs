using System;
using System.Data;
using System.Data.SqlClient;
using Quanlynhansu.Models;

namespace Quanlynhansu.DAL
{
    public class AttendanceDAL
    {
        // ========== LẤY DANH SÁCH CHẤM CÔNG THEO KHOẢNG THỜI GIAN ==========
        public static DataTable GetAttendanceByDateRange(DateTime fromDate, DateTime toDate, long? employeeID = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            a.AttendanceID,
                            a.EmployeeID,
                            e.EmployeeCode,
                            e.FullName,
                            a.AttendanceDate,
                            a.CheckInTime,
                            a.CheckOutTime,
                            a.WorkHours,
                            a.Status,
                            a.Note
                        FROM Attendance a
                        INNER JOIN Employees e ON a.EmployeeID = e.EmployeeID
                        WHERE a.AttendanceDate BETWEEN @FromDate AND @ToDate
                          AND (@EmployeeID IS NULL OR a.EmployeeID = @EmployeeID)
                        ORDER BY a.AttendanceDate DESC, e.FullName";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        cmd.Parameters.AddWithValue("@ToDate", toDate);
                        cmd.Parameters.AddWithValue("@EmployeeID", (object)employeeID ?? DBNull.Value);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy dữ liệu chấm công: " + ex.Message);
            }
            return dt;
        }

        // ========== CHẤM CÔNG VÀO ==========
        public static bool CheckIn(long employeeID, DateTime checkInTime)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra đã chấm công chưa
                    string checkQuery = @"
                        SELECT COUNT(*) 
                        FROM Attendance 
                        WHERE EmployeeID = @EmployeeID 
                          AND AttendanceDate = @AttendanceDate";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        checkCmd.Parameters.AddWithValue("@AttendanceDate", checkInTime.Date);

                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            throw new Exception("Nhân viên đã chấm công vào hôm nay!");
                        }
                    }

                    // Thêm bản ghi chấm công
                    string insertQuery = @"
                        INSERT INTO Attendance 
                        (EmployeeID, AttendanceDate, CheckInTime, Status)
                        VALUES (@EmployeeID, @AttendanceDate, @CheckInTime, N'Đang làm')";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmd.Parameters.AddWithValue("@AttendanceDate", checkInTime.Date);
                        cmd.Parameters.AddWithValue("@CheckInTime", checkInTime);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi chấm công vào: " + ex.Message);
            }
        }

        // ========== CHẤM CÔNG RA ==========
        public static bool CheckOut(long employeeID, DateTime checkOutTime)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        UPDATE Attendance SET 
                            CheckOutTime = @CheckOutTime,
                            WorkHours = DATEDIFF(MINUTE, CheckInTime, @CheckOutTime) / 60.0,
                            Status = N'Đã về'
                        WHERE EmployeeID = @EmployeeID 
                          AND AttendanceDate = @AttendanceDate
                          AND CheckOutTime IS NULL";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmd.Parameters.AddWithValue("@AttendanceDate", checkOutTime.Date);
                        cmd.Parameters.AddWithValue("@CheckOutTime", checkOutTime);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 0)
                        {
                            throw new Exception("Không tìm thấy bản ghi chấm công vào hoặc đã chấm công ra!");
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi chấm công ra: " + ex.Message);
            }
        }

        // ========== LẤY TỔNG HỢP CHẤM CÔNG THEO THÁNG ==========
        public static DataTable GetAttendanceSummary(long employeeId, int month, int year)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            COUNT(DISTINCT CAST(CheckInTime AS DATE)) AS WorkDays,
                            ISNULL(SUM(DATEDIFF(HOUR, CheckInTime, CheckOutTime)), 0) AS WorkHours
                        FROM Attendance
                        WHERE EmployeeID = @EmployeeID
                          AND MONTH(AttendanceDate) = @Month
                          AND YEAR(AttendanceDate) = @Year
                          AND CheckOutTime IS NOT NULL";

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
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy tổng hợp chấm công: " + ex.Message);
            }
        }

        // ========== LẤY CHI TIẾT CHẤM CÔNG CỦA NHÂN VIÊN ==========
        public static DataTable GetEmployeeAttendance(long employeeId, int month, int year)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        SELECT 
                            a.AttendanceID,
                            a.AttendanceDate,
                            a.CheckInTime,
                            a.CheckOutTime,
                            a.WorkHours,
                            a.Status,
                            a.Note,
                            CASE 
                                WHEN DATEPART(HOUR, a.CheckInTime) > 8 THEN N'Đi muộn'
                                WHEN a.CheckOutTime IS NULL THEN N'Chưa về'
                                WHEN DATEPART(HOUR, a.CheckOutTime) < 17 THEN N'Về sớm'
                                ELSE N'Bình thường'
                            END AS AttendanceStatus
                        FROM Attendance a
                        WHERE a.EmployeeID = @EmployeeID
                          AND MONTH(a.AttendanceDate) = @Month
                          AND YEAR(a.AttendanceDate) = @Year
                        ORDER BY a.AttendanceDate DESC";

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
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy chi tiết chấm công: " + ex.Message);
            }
        }

        // ========== CẬP NHẬT GHI CHÚ CHẤM CÔNG ==========
        public static bool UpdateAttendanceNote(long attendanceId, string note)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = @"
                        UPDATE Attendance 
                        SET Note = @Note 
                        WHERE AttendanceID = @AttendanceID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);
                        cmd.Parameters.AddWithValue("@Note", (object)note ?? DBNull.Value);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật ghi chú: " + ex.Message);
            }
        }

        // ========== XÓA CHẤM CÔNG ==========
        public static bool DeleteAttendance(long attendanceId)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = "DELETE FROM Attendance WHERE AttendanceID = @AttendanceID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AttendanceID", attendanceId);

                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa chấm công: " + ex.Message);
            }
        }
    }
}
