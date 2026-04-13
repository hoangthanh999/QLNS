using System;
using System.Data;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.BLL
{
    public class AttendanceBLL
    {
        public static DataTable GetAttendanceByDateRange(DateTime fromDate, DateTime toDate, long? employeeID = null)
        {
            try
            {
                return AttendanceDAL.GetAttendanceByDateRange(fromDate, toDate, employeeID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy dữ liệu chấm công: " + ex.Message);
            }
        }

        public static bool CheckIn(long employeeID, DateTime checkInTime)
        {
            try
            {
                return AttendanceDAL.CheckIn(employeeID, checkInTime);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi chấm công vào: " + ex.Message);
            }
        }

        public static bool CheckOut(long employeeID, DateTime checkOutTime)
        {
            try
            {
                return AttendanceDAL.CheckOut(employeeID, checkOutTime);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi chấm công ra: " + ex.Message);
            }
        }
    }
}
