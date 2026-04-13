using System;

namespace Quanlynhansu.Models
{
    public class Salary
    {
        public long SalaryID { get; set; }
        public long EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Status { get; set; }

        // Thông tin chấm công
        public int WorkDays { get; set; }
        public int StandardWorkDays { get; set; }
        public decimal WorkHours { get; set; }
        public int LeaveDays { get; set; }

        // Tính toán
        public decimal ActualSalary => BaseSalary * WorkDays / StandardWorkDays;
        public string MonthYearDisplay => $"{Month:00}/{Year}";
        public string TotalSalaryDisplay => TotalSalary.ToString("N0") + " VNĐ";
    }

    public static class SalaryStatus
    {
        public const string Draft = "Nháp";
        public const string Calculated = "Đã tính";
        public const string Approved = "Đã duyệt";
        public const string Paid = "Đã thanh toán";
        public const string Cancelled = "Đã hủy";

        public static string[] GetAll()
        {
            return new string[] { Draft, Calculated, Approved, Paid, Cancelled };
        }
    }
}
