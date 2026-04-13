using System;

namespace Quanlynhansu.Models
{
    // ========== BÁO CÁO TỔNG HỢP ==========
    public class SalarySummaryReport
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal TotalSalary { get; set; }
        public string Status { get; set; }
    }

    // ========== BÁO CÁO THEO PHÒNG BAN ==========
    public class DepartmentSalaryReport
    {
        public string DepartmentName { get; set; }
        public int EmployeeCount { get; set; }
        public decimal TotalBaseSalary { get; set; }
        public decimal TotalBonus { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AvgSalary => EmployeeCount > 0 ? TotalSalary / EmployeeCount : 0;
    }

    // ========== BÁO CÁO THEO NHÂN VIÊN ==========
    public class EmployeeSalaryReport
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal TotalSalary { get; set; }
        public string Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string MonthYear => $"{Month:00}/{Year}";
    }

    // ========== DỮ LIỆU BIỂU ĐỒ ==========
    public class SalaryChartData
    {
        public int Month { get; set; }
        public decimal TotalSalary { get; set; }
        public int EmployeeCount { get; set; }
        public decimal AvgSalary { get; set; }
        public string MonthName => $"Tháng {Month}";
    }

    // ========== THỐNG KÊ TỔNG QUAN ==========
    public class SalaryStatistics
    {
        public int TotalEmployees { get; set; }
        public decimal TotalSalaryAmount { get; set; }
        public decimal AvgSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public decimal MinSalary { get; set; }
        public decimal TotalBonus { get; set; }
        public decimal TotalDeduction { get; set; }
    }
}
