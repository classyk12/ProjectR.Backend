
using System.Text.RegularExpressions;

namespace ProjectR.Backend.Shared.Helpers

{
    public static partial class PhoneNumberHelper
    {
        /// <summary>
        /// Validates if the provided phone number is in a valid format.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>True if valid, otherwise false.</returns>
        /// 
        /// 
        public static bool IsValidPhone(string phone)
        {
            try
            {
                if (string.IsNullOrEmpty(phone))
                {
                    return false;
                }

                Regex regex = MyRegex();
                if (!regex.IsMatch(phone))
                {
                    return false;
                }

                return phone.Length switch
                {
                    < 11 or > 13 => false,
                    _ => true
                };
            }

            catch (Exception)
            {
                return false;
            }
        }

        public static string ToLongPhoneNumber(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return phone;
            }

            phone = phone.Replace(" ", string.Empty).Replace("+", string.Empty).Replace(")", "").Replace("(", "");

            if (phone.StartsWith('0'))
            {
                phone = $"234{phone[1..]}";
            }

            return phone;
        }

        public static string ToShortPhoneNumber(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return phone;
            }

            phone = phone.Replace(" ", string.Empty).Replace("+", string.Empty).Replace(")", "").Replace("(", "");

            if (phone.StartsWith("234"))
            {
                phone = $"0{phone[3..]}";
            }

            return phone;
        }

        public static bool IsValidPhoneCode(this string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(strIn,
                      @"^\d{10,15}$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        [GeneratedRegex(@"^-?[0-9][0-9,\.]+$")]
        private static partial Regex MyRegex();
    }
}