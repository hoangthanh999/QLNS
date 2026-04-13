using System;
using System.Text.RegularExpressions;

namespace Quanlynhansu.Utilities
{
    public class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Kiểm tra số điện thoại Việt Nam (10-11 số)
            string pattern = @"^(0|\+84)[0-9]{9,10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool IsValidIdentityCard(string identityCard)
        {
            if (string.IsNullOrWhiteSpace(identityCard))
                return false;

            // CMND: 9 hoặc 12 số, CCCD: 12 số
            string pattern = @"^[0-9]{9}$|^[0-9]{12}$";
            return Regex.IsMatch(identityCard, pattern);
        }

        public static bool IsNumeric(string value)
        {
            return decimal.TryParse(value, out _);
        }
    }
}
