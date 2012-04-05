using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Magic.Extensions
{
    public static class ObjectExtensions
    {
        public static T ToT<T>(this object s)
        {
            return Magically.GetSafeT<T>(s);
        }

        public static int ToInt(this object s)
        {
            int t;
            Int32.TryParse(s.ToString(), out t);

            return t;
        }

        public static Int64 ToLong(this object s)
        {
            Int64 t;
            Int64.TryParse(s.ToString(), out t);
            return t;
        }

        public static DateTime ToDateTime(this object s)
        {
            DateTime d;
            DateTime.TryParse(s.ToString(), out d);
            return d;
        }

        public static bool ToBool(this object s)
        {
            if (s == null)
                return false;
            bool b;
            bool.TryParse(s.ToString(), out b);
            return b;
        }

        public static decimal ToDecimal(this object s)
        {
            decimal d;
            decimal.TryParse(s.ToString(), out d);

            return d;
        }

        public static string ToSafeString(this object s)
        {
            if (s == null)
                return string.Empty;

            return s.ToString();
        }
    }
}
