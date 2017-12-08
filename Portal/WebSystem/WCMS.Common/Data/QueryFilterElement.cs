using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using WCMS.Common.Utilities;

namespace WCMS.Common.Data
{
    public class QueryFilterElement
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public object NullValue { get; set; }
        public QueryRelation Relation { get; set; } // Change this to enum
        public List<QueryFilterElement> Children { get; set; }

        public QueryFilterElement()
        {
            Children = new List<QueryFilterElement>();
        }

        public QueryFilterElement(string name, object value)
        {
            Name = name;
            Value = value;
            NullValue = GetDefaultValue(value.GetType());
        }

        public static object GetDefaultValue(Type type)
        {
            if (type == typeof(int))
            {
                return -1;
            }

            return null;
        }

        public bool IsValueNull()
        {
            if (this.NullValue == null && this.Value == null) return true;

            return DataHelper.CompareValues(this.Value, this.NullValue);
        }

        public override bool Equals(object obj)
        {
            if (IsValueNull())
            {
                return DataHelper.CompareValues(obj, this.NullValue);
            }

            return DataHelper.CompareValues(obj, this.Value);
        }

        public static QueryFilterElement Create(PropertyInfo prop, object value)
        {
            QueryFilterElement filter = new QueryFilterElement();
            filter.Name = prop.Name;
            filter.Value = value;
            filter.NullValue = ((ObjectColumnAttribute)prop.GetCustomAttributes(typeof(ObjectColumnAttribute), false)[0]).NullValue;

            return filter;
        }

    }

    public enum QueryRelation
    {
        And = 1,
        Or = 2
    }

    public enum FilterOperator
    {
        Equal = 1,
        GreaterThan = 2,
        LessThan = 3,
        NotEqual = 4,
        GreaterThanOrEqual = 5,
        LessThanOrEqual = 6

        // IsNull, NotNull
    }
}
