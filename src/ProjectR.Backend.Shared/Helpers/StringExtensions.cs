using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectR.Backend.Shared.Helpers
{
    public static class StringExtensions
    {
        static bool invalid = false;

        public static bool IsValidEmail(this string strIn)
        {
            invalid = false;
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsDigitOnly(this string strIn)
        {
            invalid = false;
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(strIn,
                      @"^\d+$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidURL(this string strIn)
        {
            invalid = false;
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static string DomainMapper(Match match)
        {
            IdnMapping idn = new();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }

            return match.Groups[1].Value + domainName;
        }

        public static string Base64Encode(this string plaintext)
        {
            if (string.IsNullOrWhiteSpace(plaintext))
            {
                return string.Empty;
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(base64EncodedData))
                {
                    return string.Empty;
                }

                byte[] plainTextBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(plainTextBytes);
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }
    }
}
