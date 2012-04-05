using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Magic.Extensions
{
    public static class DataTableExtensions
    {
        public static long Sum(this DataTable dt, string colName)
        {
            decimal sum = 0;
            try
            {
                if (dt.Columns[colName] == null)
                    throw new Exception("Column name is not valid");

                if (dt == null)
                    throw new Exception("Table is null");

                if (dt.Rows.Count == 0)
                    return 0;

                List<int> vals = new List<int>();

                foreach (DataRow row in dt.Rows)
                    sum += (row[colName].ToDecimal());
            }
            catch (Exception ex)
            {
            }

            return sum.ToLong();
        }

        public static List<T> Distinct<T>(this DataTable dt, string colName)
        {
            if (dt == null)
                return null;

            List<T> retVal = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                retVal.Add(Magic.Magically.GetSafeT<T>(dr[colName]));
            }

            return retVal.Distinct().ToList();
        }

        public static List<DataRow> ToDataRowList(this DataTable dt)
        {            
            List<DataRow> retVal = new List<DataRow>();

            DataTable temp = dt.Copy();

            foreach (DataRow row in temp.Rows)
            {
                retVal.Add(row);
            }

            return retVal;
        }
    }
}
