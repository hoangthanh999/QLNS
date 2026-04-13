using System;

namespace Quanlynhansu.Models
{
    public class Attendance
    {
        public long AttendanceID { get; set; }
        public long EmployeeID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public decimal? WorkHours { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
    }
}
