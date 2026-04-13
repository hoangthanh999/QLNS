using System;
using System.Data;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.BLL
{
    public class DepartmentBLL
    {
        public static DataTable GetAllDepartments()
        {
            try
            {
                return DepartmentDAL.GetAllDepartments();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách phòng ban: " + ex.Message);
            }
        }

        public static bool AddDepartment(Department dept)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(dept.DepartmentCode))
                    throw new Exception("Mã phòng ban không được để trống!");

                if (string.IsNullOrWhiteSpace(dept.DepartmentName))
                    throw new Exception("Tên phòng ban không được để trống!");

                if (dept.DepartmentCode.Length > 20)
                    throw new Exception("Mã phòng ban không được quá 20 ký tự!");

                if (dept.DepartmentName.Length > 200)
                    throw new Exception("Tên phòng ban không được quá 200 ký tự!");

                // Kiểm tra trùng mã
                if (DepartmentDAL.CheckDepartmentCodeExists(dept.DepartmentCode))
                    throw new Exception("Mã phòng ban đã tồn tại!");

                return DepartmentDAL.AddDepartment(dept);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool UpdateDepartment(Department dept)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(dept.DepartmentCode))
                    throw new Exception("Mã phòng ban không được để trống!");

                if (string.IsNullOrWhiteSpace(dept.DepartmentName))
                    throw new Exception("Tên phòng ban không được để trống!");

                if (dept.DepartmentCode.Length > 20)
                    throw new Exception("Mã phòng ban không được quá 20 ký tự!");

                if (dept.DepartmentName.Length > 200)
                    throw new Exception("Tên phòng ban không được quá 200 ký tự!");

                // Kiểm tra trùng mã (trừ chính nó)
                if (DepartmentDAL.CheckDepartmentCodeExists(dept.DepartmentCode, dept.DepartmentID))
                    throw new Exception("Mã phòng ban đã tồn tại!");

                // Kiểm tra không được chọn chính nó làm phòng ban cha
                if (dept.ParentDepartmentID.HasValue && dept.ParentDepartmentID.Value == dept.DepartmentID)
                    throw new Exception("Không thể chọn chính phòng ban này làm phòng ban cha!");

                return DepartmentDAL.UpdateDepartment(dept);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool DeleteDepartment(int departmentID)
        {
            try
            {
                return DepartmentDAL.DeleteDepartment(departmentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Department GetDepartmentById(int departmentID)
        {
            try
            {
                return DepartmentDAL.GetDepartmentById(departmentID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin phòng ban: " + ex.Message);
            }
        }

        public static DataTable GetParentDepartments()
        {
            try
            {
                return DepartmentDAL.GetParentDepartments();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách phòng ban cha: " + ex.Message);
            }
        }

        public static DataTable GetEmployeesForManager()
        {
            try
            {
                return DepartmentDAL.GetEmployeesForManager();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách nhân viên: " + ex.Message);
            }
        }
    }
}
