using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DF.Core.Components
{
    public static class AppSettings
    {
        public static readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["DFContext"].ToString();
    }
}
