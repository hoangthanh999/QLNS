using System;
using System.Data;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.BLL
{
    public class LeaveRequestBLL
    {
        public static DataTable GetAllLeaveRequests()
        {
            try
            {
                return LeaveRequestDAL.GetAllLeaveRequests();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách nghỉ phép: " + ex.Message);
            }
        }

        public static DataTable SearchLeaveRequests(string searchText = null, string status = null,
            DateTime? fromDate = null, DateTime? toDate = null, string leaveType = null)
        {
            try
            {
                return LeaveRequestDAL.SearchLeaveRequests(searchText, status, fromDate, toDate, leaveType);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm nghỉ phép: " + ex.Message);
            }
        }

        public static DataTable GetLeaveRequestsByEmployee(long employeeID)
        {
            try
            {
                return LeaveRequestDAL.GetLeaveRequestsByEmployee(employeeID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy lịch sử nghỉ phép: " + ex.Message);
            }
        }

        public static bool AddLeaveRequest(LeaveRequest leave)
        {
            try
            {
                // Validation
                ValidateLeaveRequest(leave);

                // Kiểm tra trùng lịch
                if (LeaveRequestDAL.CheckOverlappingLeave(leave.EmployeeID, leave.FromDate, leave.ToDate))
                {
                    throw new Exception("Nhân viên đã có đơn nghỉ phép trong khoảng thời gian này!");
                }

                // Tính số ngày nghỉ
                leave.TotalDays = CalculateLeaveDays(leave.FromDate, leave.ToDate);

                // Mặc định trạng thái chờ duyệt
                leave.Status = LeaveStatus.Pending;

                return LeaveRequestDAL.AddLeaveRequest(leave);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool UpdateLeaveRequest(LeaveRequest leave)
        {
            try
            {
                // Validation
                ValidateLeaveRequest(leave);

                // Kiểm tra trùng lịch (trừ chính nó)
                if (LeaveRequestDAL.CheckOverlappingLeave(leave.EmployeeID, leave.FromDate, leave.ToDate, leave.LeaveRequestID))
                {
                    throw new Exception("Nhân viên đã có đơn nghỉ phép trong khoảng thời gian này!");
                }

                // Tính lại số ngày nghỉ
                leave.TotalDays = CalculateLeaveDays(leave.FromDate, leave.ToDate);

                return LeaveRequestDAL.UpdateLeaveRequest(leave);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool ApproveLeaveRequest(long leaveRequestID, long approvedBy)
        {
            try
            {
                return LeaveRequestDAL.ApproveLeaveRequest(leaveRequestID, approvedBy, LeaveStatus.Approved);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool RejectLeaveRequest(long leaveRequestID, long approvedBy)
        {
            try
            {
                return LeaveRequestDAL.ApproveLeaveRequest(leaveRequestID, approvedBy, LeaveStatus.Rejected);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool CancelLeaveRequest(long leaveRequestID)
        {
            try
            {
                return LeaveRequestDAL.CancelLeaveRequest(leaveRequestID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool DeleteLeaveRequest(long leaveRequestID)
        {
            try
            {
                return LeaveRequestDAL.DeleteLeaveRequest(leaveRequestID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static LeaveRequest GetLeaveRequestById(long leaveRequestID)
        {
            try
            {
                return LeaveRequestDAL.GetLeaveRequestById(leaveRequestID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin đơn nghỉ phép: " + ex.Message);
            }
        }

        public static DataTable GetActiveEmployees()
        {
            try
            {
                return LeaveRequestDAL.GetActiveEmployees();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách nhân viên: " + ex.Message);
            }
        }

        public static DataTable GetLeaveStatistics(int year)
        {
            try
            {
                return LeaveRequestDAL.GetLeaveStatistics(year);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thống kê nghỉ phép: " + ex.Message);
            }
        }

        // Tính số ngày nghỉ (không tính thứ 7, CN)
        public static int CalculateLeaveDays(DateTime fromDate, DateTime toDate)
        {
            if (toDate < fromDate)
                throw new Exception("Ngày kết thúc phải sau ngày bắt đầu!");

            int days = 0;
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                // Chỉ tính thứ 2-6
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    days++;
                }
            }

            return days;
        }

        // Validation
        private static void ValidateLeaveRequest(LeaveRequest leave)
        {
            if (leave.EmployeeID <= 0)
                throw new Exception("Vui lòng chọn nhân viên!");

            if (string.IsNullOrWhiteSpace(leave.LeaveType))
                throw new Exception("Vui lòng chọn loại nghỉ phép!");

            if (leave.FromDate == DateTime.MinValue)
                throw new Exception("Vui lòng chọn ngày bắt đầu!");

            if (leave.ToDate == DateTime.MinValue)
                throw new Exception("Vui lòng chọn ngày kết thúc!");

            if (leave.ToDate < leave.FromDate)
                throw new Exception("Ngày kết thúc phải sau ngày bắt đầu!");

            if (leave.FromDate < DateTime.Today)
                throw new Exception("Không thể đăng ký nghỉ phép cho ngày trong quá khứ!");

            // Kiểm tra số ngày nghỉ hợp lý
            int totalDays = CalculateLeaveDays(leave.FromDate, leave.ToDate);
            if (totalDays <= 0)
                throw new Exception("Số ngày nghỉ phải lớn hơn 0!");

            if (totalDays > 30)
                throw new Exception("Số ngày nghỉ không được vượt quá 30 ngày!");

            if (string.IsNullOrWhiteSpace(leave.Reason))
                throw new Exception("Vui lòng nhập lý do nghỉ phép!");

            if (leave.Reason.Length > 500)
                throw new Exception("Lý do không được quá 500 ký tự!");
        }
    }
}
