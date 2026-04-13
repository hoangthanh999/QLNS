using System;
using System.Data;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;

namespace Quanlynhansu.BLL
{
    public class PositionBLL
    {
        public static DataTable GetAllPositions()
        {
            try
            {
                return PositionDAL.GetAllPositions();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách chức vụ: " + ex.Message);
            }
        }

        public static bool AddPosition(Position position)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(position.PositionCode))
                    throw new Exception("Mã chức vụ không được để trống!");

                if (string.IsNullOrWhiteSpace(position.PositionName))
                    throw new Exception("Tên chức vụ không được để trống!");

                if (position.PositionCode.Length > 20)
                    throw new Exception("Mã chức vụ không được quá 20 ký tự!");

                if (position.PositionName.Length > 200)
                    throw new Exception("Tên chức vụ không được quá 200 ký tự!");

                if (position.Level <= 0)
                    throw new Exception("Cấp bậc phải lớn hơn 0!");

                if (position.BaseSalary.HasValue && position.BaseSalary.Value < 0)
                    throw new Exception("Lương cơ bản không được âm!");

                // Kiểm tra trùng mã
                if (PositionDAL.CheckPositionCodeExists(position.PositionCode))
                    throw new Exception("Mã chức vụ đã tồn tại!");

                return PositionDAL.AddPosition(position);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool UpdatePosition(Position position)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(position.PositionCode))
                    throw new Exception("Mã chức vụ không được để trống!");

                if (string.IsNullOrWhiteSpace(position.PositionName))
                    throw new Exception("Tên chức vụ không được để trống!");

                if (position.PositionCode.Length > 20)
                    throw new Exception("Mã chức vụ không được quá 20 ký tự!");

                if (position.PositionName.Length > 200)
                    throw new Exception("Tên chức vụ không được quá 200 ký tự!");

                if (position.Level <= 0)
                    throw new Exception("Cấp bậc phải lớn hơn 0!");

                if (position.BaseSalary.HasValue && position.BaseSalary.Value < 0)
                    throw new Exception("Lương cơ bản không được âm!");

                // Kiểm tra trùng mã (trừ chính nó)
                if (PositionDAL.CheckPositionCodeExists(position.PositionCode, position.PositionID))
                    throw new Exception("Mã chức vụ đã tồn tại!");

                return PositionDAL.UpdatePosition(position);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool DeletePosition(int positionID)
        {
            try
            {
                return PositionDAL.DeletePosition(positionID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Position GetPositionById(int positionID)
        {
            try
            {
                return PositionDAL.GetPositionById(positionID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin chức vụ: " + ex.Message);
            }
        }
    }
}
