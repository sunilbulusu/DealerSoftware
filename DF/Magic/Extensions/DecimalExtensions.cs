using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Magic.Extensions
{
    public static class DecimalExtensions
    {     

        public static Int64 ToLong(this decimal s)
        {
            Int64 t;
            Int64.TryParse(s.ToString("0"), out t);
            return t;
        }
    }
}
