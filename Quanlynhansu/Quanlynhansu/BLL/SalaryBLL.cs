using System;
using System.Data;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.BLL
{
    public class SalaryBLL
    {
        // ========== TÌM KIẾM & DANH SÁCH ==========

        public static DataTable SearchSalaries(int? month, int? year, string status, string searchText)
        {
            return SalaryDAL.SearchSalaries(month, year, status, searchText);
        }

        public static Salary GetSalaryById(long salaryId)
        {
            return SalaryDAL.GetSalaryById(salaryId);
        }

        // ========== THÊM/SỬA/XÓA ==========

        public static bool AddSalary(Salary salary)
        {
            // Validation
            if (salary.EmployeeID <= 0)
                throw new Exception("Vui lòng chọn nhân viên!");

            if (salary.Month < 1 || salary.Month > 12)
                throw new Exception("Tháng không hợp lệ!");

            if (salary.Year < 2000 || salary.Year > DateTime.Now.Year + 1)
                throw new Exception("Năm không hợp lệ!");

            // Kiểm tra trùng
            if (SalaryDAL.CheckSalaryExists(salary.EmployeeID, salary.Month, salary.Year))
                throw new Exception("Lương tháng này đã tồn tại cho nhân viên này!");

            // Tính tổng lương
            salary.TotalSalary = salary.BaseSalary + salary.Bonus - salary.Deduction;

            return SalaryDAL.AddSalary(salary);
        }

        public static bool UpdateSalary(Salary salary)
        {
            // Validation
            if (salary.SalaryID <= 0)
                throw new Exception("ID lương không hợp lệ!");

            // Tính lại tổng lương
            salary.TotalSalary = salary.BaseSalary + salary.Bonus - salary.Deduction;

            return SalaryDAL.UpdateSalary(salary);
        }

        public static bool DeleteSalary(long salaryId)
        {
            // Kiểm tra trạng thái
            Salary salary = SalaryDAL.GetSalaryById(salaryId);
            if (salary == null)
                throw new Exception("Không tìm thấy bản ghi lương!");

            if (salary.Status == SalaryStatus.Paid)
                throw new Exception("Không thể xóa lương đã thanh toán!");

            return SalaryDAL.DeleteSalary(salaryId);
        }

        // ========== TÍNH LƯƠNG ==========

        public static DataTable GetEmployeesForSalaryCalculation(int month, int year, long? departmentId)
        {
            return SalaryDAL.GetEmployeesForSalaryCalculation(month, year, departmentId);
        }

        public static decimal CalculateSalary(decimal baseSalary, int workDays, int standardWorkDays,
            decimal bonus, decimal deduction)
        {
            if (standardWorkDays == 0) return 0;

            decimal actualSalary = baseSalary * workDays / standardWorkDays;
            return actualSalary + bonus - deduction;
        }

        public static int GetStandardWorkDays(int month, int year)
        {
            // Tính số ngày làm việc chuẩn (trừ T7, CN)
            int workDays = 0;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(year, month, day);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workDays++;
                }
            }

            return workDays;
        }

        // ========== BÁO CÁO & THỐNG KÊ ==========

        public static DataTable GetSalaryReport(int month, int year)
        {
            return SalaryDAL.GetSalaryReport(month, year);
        }

        public static DataTable GetSalaryStatistics(int year)
        {
            return SalaryDAL.GetSalaryStatistics(year);
        }

        // ========== CẬP NHẬT TRẠNG THÁI ==========

        public static bool ApproveSalary(long salaryId)
        {
            return SalaryDAL.UpdateSalaryStatus(salaryId, SalaryStatus.Approved);
        }

        public static bool PaySalary(long salaryId)
        {
            return SalaryDAL.UpdateSalaryStatus(salaryId, SalaryStatus.Paid, DateTime.Now);
        }

        public static bool BulkApprove(int month, int year)
        {
            return SalaryDAL.BulkUpdateStatus(month, year, SalaryStatus.Approved);
        }
    }
}
