using System;
using System.Collections.Generic;
using System.Data;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.BLL
{
    public class EmployeeBLL
    {
        public static DataTable SearchEmployees(string searchText, int? departmentID, string status, int pageNumber, int pageSize)
        {
            try
            {
                return EmployeeDAL.SearchEmployees(searchText, departmentID, status, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm nhân viên: " + ex.Message);
            }
        }
        // Cần thêm method này
        public static DataTable GetActiveEmployees()
        {
            try
            {
                return EmployeeDAL.GetActiveEmployeesForComboBox();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách nhân viên: " + ex.Message);
            }
        }

        public static Employee GetEmployeeByID(long employeeID)
        {
            try
            {
                return EmployeeDAL.GetEmployeeByID(employeeID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin nhân viên: " + ex.Message);
            }
        }

        public static bool InsertEmployee(Employee emp)
        {
            try
            {
                // Validate business rules
                if (string.IsNullOrWhiteSpace(emp.EmployeeCode))
                    throw new Exception("Mã nhân viên không được để trống!");

                if (string.IsNullOrWhiteSpace(emp.FullName))
                    throw new Exception("Họ tên không được để trống!");

                if (emp.FullName.Length < 2)
                    throw new Exception("Họ tên phải có ít nhất 2 ký tự!");

                if (emp.FullName.Length > 200)
                    throw new Exception("Họ tên không được quá 200 ký tự!");

                return EmployeeDAL.InsertEmployee(emp);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm nhân viên: " + ex.Message);
            }
        }

        public static bool UpdateEmployee(Employee emp)
        {
            try
            {
                // Validate business rules
                if (string.IsNullOrWhiteSpace(emp.FullName))
                    throw new Exception("Họ tên không được để trống!");

                if (emp.FullName.Length < 2)
                    throw new Exception("Họ tên phải có ít nhất 2 ký tự!");

                return EmployeeDAL.UpdateEmployee(emp);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật nhân viên: " + ex.Message);
            }
        }

        public static bool DeleteEmployee(long employeeID)
        {
            try
            {
                return EmployeeDAL.DeleteEmployee(employeeID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa nhân viên: " + ex.Message);
            }
        }

        public static long GetTotalEmployees(string searchText, int? departmentID, string status)
        {
            try
            {
                return EmployeeDAL.GetTotalEmployees(searchText, departmentID, status);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đếm nhân viên: " + ex.Message);
            }
        }
        // ========== KIỂM TRA MÃ NHÂN VIÊN ==========
        public static bool IsEmployeeCodeExists(string employeeCode)
        {
            try
            {
                return EmployeeDAL.IsEmployeeCodeExists(employeeCode);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra mã nhân viên: " + ex.Message);
            }
        }

        // ========== VALIDATE DỮ LIỆU IMPORT ==========
        public static List<string> ValidateImportData(List<Employee> employees)
        {
            List<string> errors = new List<string>();
            HashSet<string> employeeCodes = new HashSet<string>();

            for (int i = 0; i < employees.Count; i++)
            {
                Employee emp = employees[i];
                int rowNumber = i + 2; // +2 vì có header và bắt đầu từ 1

                // Kiểm tra mã nhân viên
                if (string.IsNullOrWhiteSpace(emp.EmployeeCode))
                    errors.Add($"Dòng {rowNumber}: Mã nhân viên không được để trống");
                else if (employeeCodes.Contains(emp.EmployeeCode))
                    errors.Add($"Dòng {rowNumber}: Mã nhân viên '{emp.EmployeeCode}' bị trùng trong file");
                else if (IsEmployeeCodeExists(emp.EmployeeCode))
                    errors.Add($"Dòng {rowNumber}: Mã nhân viên '{emp.EmployeeCode}' đã tồn tại trong hệ thống");
                else
                    employeeCodes.Add(emp.EmployeeCode);

                // Kiểm tra họ tên
                if (string.IsNullOrWhiteSpace(emp.FullName))
                    errors.Add($"Dòng {rowNumber}: Họ tên không được để trống");
                else if (emp.FullName.Length < 2)
                    errors.Add($"Dòng {rowNumber}: Họ tên phải có ít nhất 2 ký tự");

                // Kiểm tra giới tính
                if (!string.IsNullOrEmpty(emp.Gender) && emp.Gender != "Nam" && emp.Gender != "Nữ")
                    errors.Add($"Dòng {rowNumber}: Giới tính phải là 'Nam' hoặc 'Nữ'");

                // Kiểm tra email
                if (!string.IsNullOrEmpty(emp.Email) && !emp.Email.Contains("@"))
                    errors.Add($"Dòng {rowNumber}: Email không hợp lệ");

                // Kiểm tra trạng thái
                if (!string.IsNullOrEmpty(emp.Status))
                {
                    if (emp.Status != "Đang làm việc" && emp.Status != "Nghỉ việc" && emp.Status != "Tạm nghỉ")
                        errors.Add($"Dòng {rowNumber}: Trạng thái không hợp lệ");
                }
            }

            return errors;
        }

        // ========== IMPORT NHÂN VIÊN ==========
        public static int BulkInsertEmployees(List<Employee> employees)
        {
            try
            {
                return EmployeeDAL.BulkInsertEmployees(employees);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi import nhân viên: " + ex.Message);
            }
        }

    }
}
