using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Magic
{
    public class Config
    {
        public static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ToString();
        }

        public static string GetAppSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName].ToString();
        }
    }
}
