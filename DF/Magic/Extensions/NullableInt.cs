using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magic.Extensions
{
    public static class NullableInt
    {
        public static int ToInt(this int? val)
        {
            return Magically.GetSafeT<int>(val);
        }
    }
}
