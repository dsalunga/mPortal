using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

namespace WCMS.Common.Utilities
{
    public abstract class DataHelper : DataUtil
    {

    }

    /// <summary>
    /// Summary description for DataHelper
    /// </summary>
    public abstract class DataUtil
    {
        private static readonly DateTime _minDate = new DateTime(1910, 1, 1);

        public static DateTime MinDate { get { return _minDate; } }

        #region GetId

        public static int GetId(string s, int defaultValue = -1)
        {
            int id = defaultValue;

            if (!string.IsNullOrEmpty(s))
                int.TryParse(s, out id);

            return GetId(id, defaultValue);
        }

        public static int GetId(int id, int defaultValue = -1)
        {
            return id < 1 ? defaultValue : id;
        }

        public static int GetId(object o, int defaultValue = -1, bool nativeValue = false)
        {
            if (o != null)
                return nativeValue ? GetId((int)o) : GetId(o.ToString());

            return defaultValue;
        }

        public static int GetId(IDataReader r, string key, bool nativeValue = false)
        {
            return GetId(r[key], nativeValue: nativeValue);
        }

        public static int GetId(HttpSessionState r, string key)
        {
            return GetId(r[key]);
        }

        public static int GetId(HttpRequest r, string key)
        {
            return GetId(r[key]);
        }

        public static int GetId(HttpRequestBase r, string key)
        {
            return GetId(r[key]);
        }

        #endregion

        #region Get

        public static string Get(IDataReader r, string key)
        {
            return r[key].ToString();
        }

        public static string Get(DataRow r, string key)
        {
            return r[key].ToString();
        }

        public static string Get(HttpSessionState r, string key)
        {
            var value = r[key];
            if (value != null)
                return value.ToString();

            return null;
        }

        public static string Get(HttpRequest r, string key)
        {
            return r[key];
        }

        public static string Get(HttpRequestBase r, string key)
        {
            return r[key];
        }

        #endregion

        #region GetInt32

        public static int GetInt32(object o)
        {
            return GetInt32(o.ToString());
        }

        public static int GetInt32(object o, int defaultValue, bool nativeValue = false)
        {
            if (o == null)
                return defaultValue;

            return nativeValue ? (int)o : GetInt32(o.ToString(), defaultValue);
        }

        public static int GetInt32(string o)
        {
            int value;
            if (!Int32.TryParse(o, out value))
                value = 0;

            return value;
        }

        public static int GetInt32(IDataReader r, string key, bool nativeValue = false, int defaultValue = 0)
        {
            return GetInt32(r[key], defaultValue, nativeValue);
        }

        public static int GetInt32(HttpRequest r, string key, int defaultValue = 0)
        {
            return GetInt32(r[key], defaultValue);
        }

        public static int GetInt32(HttpRequestBase r, string key, int defaultValue = 0)
        {
            return GetInt32(r[key], defaultValue);
        }

        public static int GetInt32(string o, int defaultValue)
        {
            if (string.IsNullOrEmpty(o))
                return defaultValue;

            return GetInt32(o);
        }

        #endregion

        #region GetInt64

        public static Int64 GetInt64(object o)
        {
            return GetInt64(o.ToString());
        }

        public static Int64 GetInt64(object o, Int64 defaultValue)
        {
            if (o == null)
                return defaultValue;

            return GetInt64(o.ToString(), defaultValue);
        }

        public static Int64 GetInt64(string o)
        {
            Int64 value;
            if (!Int64.TryParse(o, out value))
                value = 0;

            return value;
        }

        public static Int64 GetInt64(IDataReader r, string key)
        {
            return GetInt64(r[key]);
        }

        public static Int64 GetInt64(HttpRequest r, string key)
        {
            return GetInt64(r[key]);
        }

        public static Int64 GetInt64(string o, Int64 defaultValue)
        {
            if (string.IsNullOrEmpty(o))
                return defaultValue;

            return GetInt64(o);
        }

        #endregion

        #region GetLong

        public static long GetLong(string o)
        {
            long value;
            if (!long.TryParse(o, out value))
                value = 0;

            return value;
        }

        public static long GetLong(string o, long defaultValue)
        {
            if (string.IsNullOrEmpty(o))
                return defaultValue;

            return GetLong(o);
        }

        public static long GetLong(object o, long defaultValue, bool nativeValue = false)
        {
            if (o == null)
                return defaultValue;

            return nativeValue ? (long)o : GetLong(o.ToString(), defaultValue);
        }

        public static long GetLong(IDataReader r, string key, bool nativeValue = false, long defaultValue = 0)
        {
            return GetLong(r[key], defaultValue, nativeValue);
        }

        #endregion

        #region GetDouble

        public static double GetDouble(string o)
        {
            double value;
            if (!double.TryParse(o, out value))
                value = 0;

            return value;
        }

        public static double GetDouble(string o, double defaultValue)
        {
            if (string.IsNullOrEmpty(o))
                return defaultValue;

            return GetDouble(o);
        }

        public static double GetDouble(object o, double defaultValue, bool nativeValue = false)
        {
            if (o == null)
                return defaultValue;

            return nativeValue ? (double)o : GetDouble(o.ToString(), defaultValue);
        }

        public static double GetDouble(IDataReader r, string key, bool nativeValue = false, double defaultValue = 0)
        {
            return GetDouble(r[key], defaultValue, nativeValue);
        }

        #endregion

        #region GetChar

        public static char GetChar(object o)
        {
            return GetChar(o.ToString());
        }

        public static char GetChar(string o)
        {
            char value;
            if (!char.TryParse(o, out value))
                value = char.MinValue;

            return value;
        }

        public static char GetChar(IDataReader r, string key)
        {
            return GetChar(r[key]);
        }

        public static char GetChar(HttpRequest r, string key)
        {
            return GetChar(r[key]);
        }

        #endregion

        #region IsTrue

        public static bool IsTrue(object o)
        {
            return o != null && IsTrue(o.ToString());
        }

        public static bool IsTrue(string o)
        {
            return o != null && (o == "1" || o.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
                o.Equals("yes", StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion

        #region GetDateTime

        public static DateTime GetDateTime(object o)
        {
            return GetDateTime(o.ToString());
        }

        public static DateTime GetDateTime(object o, DateTime defaultValue)
        {
            return GetDateTime(o.ToString(), defaultValue);
        }

        public static DateTime GetDateTime(IDataReader r, string key)
        {
            var dateTime = r[key];
            if (dateTime == DBNull.Value)
                return _minDate;

            return (DateTime)dateTime;
        }

        public static DateTime GetDateTime(string dateString)
        {
            DateTime value;
            if (!DateTime.TryParse(dateString, out value))
                value = _minDate;

            return value;
        }

        public static DateTime GetDateTime(string dateString, IFormatProvider provider)
        {
            return GetDateTime(dateString, provider, _minDate);
        }

        public static DateTime GetDateTime(string dateString, IFormatProvider provider, DateTime defaultValue)
        {
            DateTime value;

            try
            {
                value = DateTime.Parse(dateString, provider);
            }
            catch
            {
                value = defaultValue;
            }

            return value;
        }

        public static DateTime GetDateTime(string dateString, DateTime defaultValue)
        {
            DateTime value;
            if (!DateTime.TryParse(dateString, out value))
                value = defaultValue;

            return value;
        }

        #endregion

        #region ToDataSet

        public static DataSet ToDataSet<T>(List<T> items) // where T : IList<T>
        {
            return ToDataSet(items, "Default");
        }

        public static DataSet ToDataSet<T>(T item)
        {
            List<T> items = new List<T>();
            items.Add(item);

            return ToDataSet(items);
        }

        public static DataSet ToDataSet<T>(List<T> items, string tableName) // where T : IList<T>
        {
            return ToDataSet(items.AsEnumerable(), tableName);
        }

        public static DataSet ToDataSet<T>(IEnumerable<T> items) // where T : IList<T>
        {
            return ToDataSet(items, "Default");
        }

        public static DataSet ToDataSet<T>(IEnumerable<T> items, string tableName) // where T : IList<T>
        {
            DataSet ds = new DataSet();
            DataTable table = ds.Tables.Add(tableName);

            Type itemType = typeof(T);
            var props = itemType.GetProperties().Where(i => i.CanRead);
            foreach (PropertyInfo prop in props)
            {
                table.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));

                // Future: Implement caching DataTable definition for performance
            }

            foreach (T item in items)
            {
                if (item != null)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyInfo prop in props)
                        if (prop.Name != "Item") // This is to ignore the indexer property
                            row[prop.Name] = prop.GetValue(item, null);

                    table.Rows.Add(row);
                }
            }

            return ds;
        }

        #endregion

        #region GetBool

        public static bool GetBool(string o, bool valueIfNull = false)
        {
            if (string.IsNullOrWhiteSpace(o))
                return valueIfNull;

            return (o == "1" || o.Equals("true", StringComparison.InvariantCultureIgnoreCase) ||
                o.Equals("yes", StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool GetBool(HttpRequest r, string key, bool defaultValue = false)
        {
            return GetBool(r[key], defaultValue);
        }

        public static bool GetBool(NameValueCollection r, string key, bool defaultValue = false)
        {
            return GetBool(r[key], defaultValue);
        }

        public static bool GetBool(object o, bool defaultValue = false)
        {
            return o == null ? defaultValue : GetBool(o.ToString(), defaultValue);
        }

        #endregion

        public static dynamic ToDynamic(object o)
        {
            return o;
        }

        public static bool IsPresent(string toSearch, IEnumerable<string> values, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            foreach (var value in values)
            {
                if (value.Equals(toSearch, comparison))
                    return true;
            }

            return false;
        }

        public static bool CompareValues(object o1, object o2)
        {
            if (o1 == null && o2 == null)
                return true;

            if (o1 == null || o2 == null)
                return false;

            Type oType = o1.GetType();
            if (oType == o2.GetType())
            {
                if (oType == typeof(int))
                    return (int)o1 == (int)o2;
                else if (oType == typeof(DateTime))
                    return ((DateTime)o1).Ticks == ((DateTime)o2).Ticks;
            }

            return o1.ToString() == o2.ToString();
        }

        public static bool HasMatch(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;

            if (s1.IndexOf(s2, StringComparison.InvariantCultureIgnoreCase) >= 0)
                return true;

            if (s2.IndexOf(s1, StringComparison.InvariantCultureIgnoreCase) >= 0)
                return true;

            return false;
        }

        public static string GetStringPreview(string content, int previewChars = 50)
        {
            if (string.IsNullOrEmpty(content) || content.Length <= previewChars)
                return content;

            return content.Length > previewChars ? content.Substring(0, previewChars).Replace(Environment.NewLine, " ") + "..." : content;
        }

        public static string FormatString(string s, string defaultValue = "", string format = null)
        {
            if (string.IsNullOrEmpty(s))
                return defaultValue;
            else if (string.IsNullOrEmpty(format))
                return s;
            else
                return string.Format(format, s);
        }

        public static IEnumerable<T> PageWithSort<T>(IEnumerable<T> items, string sortBy, int startRowIndex, int maximumRows)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return items
                    .Skip(startRowIndex)
                    .Take(maximumRows);
            }
            else
            {
                var param = Expression.Parameter(typeof(T), "item");
                const string CONST_DESC = " DESC";

                bool isSortDesc = sortBy.EndsWith(CONST_DESC);
                string sortField = isSortDesc ? sortBy.Substring(0, sortBy.Length - CONST_DESC.Length) : sortBy;

                var sortExpression = Expression.Lambda<Func<T, object>>
                    (Expression.Convert(Expression.Property(param, sortField), typeof(object)), param);

                if (isSortDesc)
                    return items.AsQueryable<T>()
                        .OrderByDescending<T, object>(sortExpression)
                        .Skip(startRowIndex)
                        .Take(maximumRows);
                else
                    return items.AsQueryable<T>()
                        .OrderBy<T, object>(sortExpression)
                        .Skip(startRowIndex)
                        .Take(maximumRows);
            }
        }

        public static DataSet GetEmptyDataSet()
        {
            DataSet ds = new DataSet();
            DataTable table = ds.Tables.Add();

            return ds;
        }

        public static List<int> ParseCommaSeparatedIdList(string ids)
        {
            List<int> idList = new List<int>();
            string[] idArray = ids.Split(',');
            foreach (string idString in idArray)
            {
                int id = GetId(idString);
                if (id > 0)
                    idList.Add(id);
            }

            return idList;
        }

        public static List<string> ParseDelimitedStringToList(string stringValue, char separator = ',')
        {
            List<string> stringList = new List<string>();

            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                string[] stringArray = stringValue.Split(separator);
                foreach (string idString in stringArray)
                {
                    string value = idString.Trim();
                    if (!string.IsNullOrEmpty(value))
                        stringList.Add(value);
                }
            }

            return stringList;
        }

        public static string ToDelimitedString(List<string> items, char delimiter)
        {
            if (items.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(items[0].Trim());

                if (items.Count > 1)
                    for (int i = 1; i < items.Count; i++)
                        sb.Append(delimiter + items[i].Trim());

                return sb.ToString();
            }

            return string.Empty;
        }

        public static string ParseToOracleRowId(string s)
        {
            return !string.IsNullOrEmpty(s) ? s.Trim().Replace(" ", "+") : s;
        }

        public static string FormatPhoneNumber(string number)
        {
            if (number != null)
                return number.Trim().Replace("-", "").Replace(" ", "");

            return number;
        }

        public static object GetValue(PropertyInfo prop, string value)
        {
            switch (prop.PropertyType.Name)
            {
                case "Int32":
                    return GetInt32(value);

                case "DateTime":
                    return GetDateTime(value);

                case "Boolean":
                    return GetBool(value);

                default:
                    return value;
            }
        }

        public static List<T> FromXml<T>(string xml, string itemName = "", bool isList = true)
        {
            List<T> items = new List<T>();

            var itemType = typeof(T);
            var props = itemType.GetProperties().Where(i => i.CanWrite).ToDictionary(i => i.Name);
            var itemNode = string.IsNullOrEmpty(itemName) ? itemType.Name : itemName;

            StringReader stringReader = new StringReader(xml);

            XmlReader reader = XmlReader.Create(stringReader);

            if (isList)
                reader.Read();

            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name.Equals(itemNode))
                {
                    var item = (T)Activator.CreateInstance(itemType);

                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && props.ContainsKey(reader.Name))
                        {
                            var prop = props[reader.Name];

                            reader.Read();

                            prop.SetValue(item, GetValue(prop, reader.Value), null);

                            reader.Read();
                        }
                        else if (reader.Name.Equals(itemNode))
                        {
                            break;
                        }
                    }

                    items.Add(item);
                }
            }

            return items;
        }

        public static T FromElementXml<T>(string xml, string itemName = "")
        {
            var itemType = typeof(T);
            var props = itemType.GetProperties().Where(i => i.CanWrite).ToDictionary(i => i.Name);
            var itemNode = string.IsNullOrEmpty(itemName) ? itemType.Name : itemName;

            StringReader stringReader = new StringReader(xml);

            XmlReader reader = XmlReader.Create(stringReader);

            while (reader.Read())
            {
                if (reader.IsStartElement() && reader.Name.Equals(itemNode))
                {
                    var item = (T)Activator.CreateInstance(itemType);

                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && !reader.IsEmptyElement && props.ContainsKey(reader.Name))
                        {
                            var prop = props[reader.Name];

                            reader.Read();

                            prop.SetValue(item, GetValue(prop, reader.Value), null);

                            reader.Read();
                        }
                        else if (reader.Name.Equals(itemNode))
                        {
                            break;
                        }
                    }

                    return item;
                }
            }

            return default(T);
        }

        public static string ToXml<T>(IEnumerable<T> items, string itemName = "", string parentName = "") // where T : IList<T>
        {
            bool hasParentNode = !string.IsNullOrEmpty(parentName);

            StringBuilder output = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.NewLineOnAttributes = false;
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;
            settings.Encoding = Encoding.Unicode;

            XmlWriter writer = XmlWriter.Create(output, settings);

            if (hasParentNode)
                writer.WriteStartElement(parentName);

            if (items.Count() > 0)
            {
                var itemType = typeof(T);
                var props = itemType.GetProperties().Where(i => i.CanWrite);
                var itemNode = string.IsNullOrEmpty(itemName) ? itemType.Name : itemName;

                foreach (T item in items)
                {
                    if (item != null)
                    {
                        writer.WriteStartElement(itemNode);

                        foreach (PropertyInfo prop in props)
                            if (prop.Name != "Item") // This is to ignore the indexer property
                                writer.WriteElementString(prop.Name, ToString(prop.GetValue(item, null)));

                        writer.WriteEndElement();
                    }
                }
            }

            if (hasParentNode)
                writer.WriteEndElement();

            writer.Flush();

            return output.ToString();
        }

        public static string ToXml<T>(T item, string itemName = "", string parentName = "")
        {
            StringBuilder output = new StringBuilder();

            if (item != null)
            {
                bool hasParentNode = !string.IsNullOrEmpty(parentName);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.NewLineOnAttributes = false;
                settings.Indent = false;
                settings.OmitXmlDeclaration = true;
                settings.Encoding = Encoding.Unicode;

                XmlWriter writer = XmlWriter.Create(output, settings);

                var itemType = typeof(T);
                var props = itemType.GetProperties().Where(i => i.CanWrite);
                var itemNode = string.IsNullOrEmpty(itemName) ? itemType.Name : itemName;

                if (hasParentNode)
                    writer.WriteStartElement(parentName);

                writer.WriteStartElement(itemNode);

                foreach (PropertyInfo prop in props)
                    if (prop.Name != "Item") // This is to ignore the indexer property
                        writer.WriteElementString(prop.Name, ToString(prop.GetValue(item, null)));

                writer.WriteEndElement();

                if (hasParentNode)
                    writer.WriteEndElement();

                writer.Flush();
            }

            return output.ToString();
        }

        public static string ToString(bool value, BoolStrings strings = BoolStrings.ZeroOne)
        {
            if (strings == BoolStrings.TrueFalse)
                return value ? "True" : "False";
            else if (strings == BoolStrings.YesNo)
                return value ? "Yes" : "No";
            else
                return value ? "1" : "0";
        }

        public static string ToString(object o)
        {
            return o == null ? null : o.ToString();
        }

        public static string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string ReadNextWord(StringReader reader)
        {
            char peakChar;
            while ((peakChar = (char)reader.Peek()) == ' ')
            {
                reader.Read();
            }

            string block = "";
            while ((peakChar = (char)reader.Peek()) != ' ')
            {
                block += (char)reader.Read();
            }

            return block;
        }

        /// <summary>
        /// Shuffle the array.
        /// </summary>
        /// <typeparam name="T">Array element type.</typeparam>
        /// <param name="array">Array to shuffle.</param>
        public static void Shuffle<T>(T[] array)
        {
            var random = new Random();
            for (int i = array.Length; i > 1; i--)
            {
                // Pick random element to swap.
                int j = random.Next(i); // 0 <= j <= i-1
                // Swap.
                T tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }
    }

    public enum BoolStrings
    {
        ZeroOne = 1,
        YesNo = 2,
        TrueFalse = 3
    }
}