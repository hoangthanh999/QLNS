using System;
using System.Data;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.BLL
{
    public class SalaryReportBLL
    {
        // ========== BÁO CÁO TỔNG HỢP ==========
        public static DataTable GetSalarySummaryReport(int month, int year)
        {
            try
            {
                if (month < 1 || month > 12)
                    throw new Exception("Tháng không hợp lệ!");

                if (year < 2000 || year > DateTime.Now.Year)
                    throw new Exception("Năm không hợp lệ!");

                return SalaryReportDAL.GetSalarySummaryReport(month, year);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ========== BÁO CÁO THEO PHÒNG BAN ==========
        public static DataTable GetSalaryByDepartmentReport(int month, int year)
        {
            try
            {
                if (month < 1 || month > 12)
                    throw new Exception("Tháng không hợp lệ!");

                if (year < 2000 || year > DateTime.Now.Year)
                    throw new Exception("Năm không hợp lệ!");

                return SalaryReportDAL.GetSalaryByDepartmentReport(month, year);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ========== BÁO CÁO THEO NHÂN VIÊN ==========
        public static DataTable GetSalaryByEmployeeReport(long employeeId, int fromMonth, int fromYear, int toMonth, int toYear)
        {
            try
            {
                if (employeeId <= 0)
                    throw new Exception("Vui lòng chọn nhân viên!");

                if (fromYear > toYear || (fromYear == toYear && fromMonth > toMonth))
                    throw new Exception("Khoảng thời gian không hợp lệ!");

                return SalaryReportDAL.GetSalaryByEmployeeReport(employeeId, fromMonth, fromYear, toMonth, toYear);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ========== DỮ LIỆU BIỂU ĐỒ ==========
        public static DataTable GetSalaryChartData(int year)
        {
            try
            {
                if (year < 2000 || year > DateTime.Now.Year)
                    throw new Exception("Năm không hợp lệ!");

                return SalaryReportDAL.GetSalaryChartData(year);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ========== THỐNG KÊ TỔNG QUAN ==========
        public static SalaryStatistics GetSalaryStatistics(int month, int year)
        {
            try
            {
                if (month < 1 || month > 12)
                    throw new Exception("Tháng không hợp lệ!");

                if (year < 2000 || year > DateTime.Now.Year)
                    throw new Exception("Năm không hợp lệ!");

                return SalaryReportDAL.GetSalaryStatistics(month, year);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ========== SO SÁNH THEO THÁNG ==========
        public static DataTable CompareSalaryByMonth(int year)
        {
            try
            {
                if (year < 2000 || year > DateTime.Now.Year)
                    throw new Exception("Năm không hợp lệ!");

                return SalaryReportDAL.CompareSalaryByMonth(year);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ========== TOP NHÂN VIÊN ==========
        public static DataTable GetTopSalaryEmployees(int month, int year, int topCount = 10)
        {
            try
            {
                if (month < 1 || month > 12)
                    throw new Exception("Tháng không hợp lệ!");

                if (year < 2000 || year > DateTime.Now.Year)
                    throw new Exception("Năm không hợp lệ!");

                if (topCount < 1 || topCount > 100)
                    throw new Exception("Số lượng không hợp lệ!");

                return SalaryReportDAL.GetTopSalaryEmployees(month, year, topCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
