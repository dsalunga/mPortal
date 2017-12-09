using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ObjectColumnAttribute : Attribute
    {
        public ObjectColumnAttribute(bool isPrimaryKey)
        {
            this.IsPrimaryKey = isPrimaryKey;
        }

        public ObjectColumnAttribute(bool isPrimaryKey, string mappingName)
            : this(isPrimaryKey)
        {
            MappingName = mappingName;
        }

        public ObjectColumnAttribute(bool isPrimaryKey, bool isRequired)
        {
            this.IsPrimaryKey = isPrimaryKey;
            this.IsRequired = IsRequired;
        }

        public ObjectColumnAttribute()
            : this(false, false)
        {
            NullValue = null;
        }

        public bool IsPrimaryKey { get; set; }
        public object NullValue { get; set; }
        public string MappingName { get; set; }
        public int MaxLength { get; set; }
        public bool IsRequired { get; set; }

        // DbTypeMapping = text, varchar, clob, etc, int/bit
    }
}
