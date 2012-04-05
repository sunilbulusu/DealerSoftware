using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Magic.Extensions
{
    public static class StringExtensions
    {
        public static string FormatPhoneNumber(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;

            Regex regex = new Regex(@"\D");
            s = regex.Replace(s,"");

            Regex regexPhone = new Regex(@"(\d{3})(\d{3})(\d{4})");
            s = regexPhone.Replace(s, "$1.$2.$3");

            return s;
        }

        public static bool HasValue(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static string FormatDate(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;

            DateTime dt;
            DateTime.TryParse(s, out dt);

            return dt.ToString("MM/dd/yyyy");
        }

        public static int ToInt(this string s)
        {
            int val = 0;
            Int32.TryParse(s, out val);
            return val;
        }

        public static decimal ToDecimal(this string s)
        {
            decimal val = 0;
            decimal.TryParse(s, out val);
            return val;
        }

        public static bool ToBool(this string s)
        {
            bool val = false;
            bool.TryParse(s, out val);
            return val;
        }

        public static T ToT<T>(this string s)
        {
            return Magically.GetSafeT<T>(s);
        }

        public static Int64 ToLong(this string s)
        {
            long val = 0;
            long.TryParse(s, out val);
            return val;
        }

        public static DateTime ToDateTime(this string s)
        {
            DateTime dt;
            DateTime.TryParse(s, out dt);
            return dt;
        }

        public static Guid ToGuid(this string s)
        {
            Guid guid;
            Guid.TryParse(s, out guid);
            return guid;
        }

        public static String MaxLength(this string s,int maxLength)
        {
            if (!string.IsNullOrWhiteSpace(s) && maxLength > 0 && maxLength < s.Length)
                return s.Substring(0, maxLength);
            else
                return s;
        }

        public static string Left(this string s, int length)
        {
            if (!string.IsNullOrWhiteSpace(s) && length > 0 && length < s.Length)
                return s.Substring(0, length);
            else
                return s;
        }

        public static string Right(this string s, int length)
        {
            if (!string.IsNullOrWhiteSpace(s) && length > 0 && length < s.Length)
                return new string(new string(s.Reverse().ToArray()).Substring(0, length).Reverse().ToArray());
            else
                return s;
        }

        public static string RightWithAsterisk(this string s, int length)
        {
            if (string.IsNullOrWhiteSpace(s) || length == 0 || length > s.Length)
                return s;

            int len = s.Length;

            s = s.Right(length);

            for (int i = 0; i < len - length; i++)
                s = "*" + s;

            return s;

        }
    }

    public static class DateTimeExtensions
    {
        public static string ToFormattedString(this DateTime d)
        {
            return d.ToString().FormatDate();
        }

        public static string ToFormattedString(this DateTime? d)
        {
            if (d.HasValue)
                return d.Value.ToString().FormatDate();

            return string.Empty;
        }
    }
}
