using System;

namespace Quanlynhansu.Models
{
    public class LeaveRequest
    {
        public long LeaveRequestID { get; set; }
        public long EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public long? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    // Enum cho loại nghỉ phép
    public static class LeaveTypes
    {
        public const string AnnualLeave = "Nghỉ phép năm";
        public const string SickLeave = "Nghỉ ốm";
        public const string MaternityLeave = "Nghỉ thai sản";
        public const string UnpaidLeave = "Nghỉ không lương";
        public const string MarriageLeave = "Nghỉ kết hôn";
        public const string FuneralLeave = "Nghỉ tang";
        public const string Other = "Khác";

        public static string[] GetAll()
        {
            return new string[]
            {
                AnnualLeave,
                SickLeave,
                MaternityLeave,
                UnpaidLeave,
                MarriageLeave,
                FuneralLeave,
                Other
            };
        }
    }

    // Enum cho trạng thái
    public static class LeaveStatus
    {
        public const string Pending = "Chờ duyệt";
        public const string Approved = "Đã duyệt";
        public const string Rejected = "Từ chối";
        public const string Cancelled = "Đã hủy";

        public static string[] GetAll()
        {
            return new string[]
            {
                Pending,
                Approved,
                Rejected,
                Cancelled
            };
        }
    }
}
