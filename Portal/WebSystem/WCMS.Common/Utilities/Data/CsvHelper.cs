using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using System.Data;
using System.IO;

namespace WCMS.Common.Utilities
{
    public abstract class CsvHelper
    {
        public static decimal ToDecimal(object obj)
        {
            return obj is DBNull ? 0 : Convert.ToDecimal(obj);
        }

        public static string FormatShortDate(object o)
        {
            if (o is DBNull)
                return "\"\",";

            DateTime date = Convert.ToDateTime(o);
            return "\"" + date.ToString("dd/MM/yyyy") + "\",";
        }

        public static string FormatCurrency(object o)
        {
            if (o is DBNull)
                return "\"$" + FillSpaces("0.00", 14) + "\",";

            decimal d = Convert.ToDecimal(o);
            return "\"$" + FillSpaces(d.ToString("N"), 14) + "\",";

        }

        public static string FormatNumber(decimal d, int digits)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            //nfi.CurrencySymbol = "$";
            nfi.NumberDecimalDigits = digits;

            return "\"" + d.ToString("N", nfi) + "\",";
        }

        public static string Format(object o)
        {
            return "\"" + o + "\",";
        }

        public static string Format(string s)
        {
            return "\"" + s + "\",";
        }

        public static int DateToInteger(DateTime date)
        {
            return Convert.ToInt32(date.ToString("yyyyMMdd"));
        }

        private static string FillSpaces(string s, int total)
        {
            return s.Length <= total ? s.PadLeft(total) : s;
        }

        public static bool CreateCsvFile(DataTable dt, string targetPath)
        {
            try
            {
                string folder = Path.GetDirectoryName(targetPath);
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                using (TextWriter writer = new StreamWriter(targetPath, false))
                {
                    int colCount = dt.Columns.Count;

                    //extract the header information
                    foreach (DataColumn c in dt.Columns)
                        writer.Write(c.ColumnName + ",");

                    writer.WriteLine();

                    //extract the data and write line by line to the CSV file
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < colCount; i++)
                            writer.Write(CsvHelper.Format(row[i])); //row[i] + ",");

                        writer.WriteLine();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }

            return false;
        }
    }
}
