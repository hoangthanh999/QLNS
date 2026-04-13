using System;
using Quanlynhansu.DAL;
using Quanlynhansu.Models;
using Quanlynhansu.Utilities;

namespace Quanlynhansu.BLL
{
    public class UserBLL
    {
        public static User Login(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    throw new Exception("Tên đăng nhập không được để trống!");

                if (string.IsNullOrWhiteSpace(password))
                    throw new Exception("Mật khẩu không được để trống!");

                string passwordHash = SecurityHelper.HashPassword(password);
                return UserDAL.Login(username, passwordHash);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đăng nhập: " + ex.Message);
            }
        }
    }
}
