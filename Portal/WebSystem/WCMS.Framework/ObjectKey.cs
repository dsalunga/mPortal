using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    [Serializable]
    public class ObjectKey
    {
        public const string KeyString = "Key";
        public const string KeySource = "Source";

        public int ObjectId { get; set; }
        public int RecordId { get; set; }

        #region Constructors

        public ObjectKey()
        {
            ObjectId = -1;
            RecordId = -1;
        }

        public ObjectKey(string keyString, char separator = '-')
        {
            Decode(keyString, separator);
        }

        public ObjectKey(int objectCode)
        {
            Decode(objectCode);
        }

        public ObjectKey(int objectId, int recordId)
        {
            ObjectId = objectId;
            RecordId = recordId;
        }

        public ObjectKey(IWebObject item)
        {
            ObjectId = item.OBJECT_ID;
            RecordId = item.Id;
        }

        #endregion

        #region Experimental

        public int UniqueId { get; set; }

        public IWebObject Object { get { return null; } }

        public IWebObject Record
        {
            get
            {
                switch (ObjectId)
                {
                    case WebObjects.WebSite:
                        return WSite.Get(RecordId);

                    case WebObjects.WebPage:
                        return WPage.Get(RecordId);

                    case WebObjects.WebMasterPage:
                        return WebMasterPage.Get(RecordId);

                    case WebObjects.WebPageElement:
                        return WebPageElement.Get(RecordId);

                    default:
                        return null;
                }
            }
        }

        #endregion

        public void Decode(string keyString, char separator = '-')
        {
            if (IsValid(keyString, separator.ToString()))
            {
                string[] keys = keyString.Split(separator);
                ObjectId = keys.Length > 0 ? DataHelper.GetId(keys[0]) : -1;
                RecordId = keys.Length > 1 ? DataHelper.GetId(keys[1]) : -1;
            }
            else
            {
                ObjectId = -1;
                RecordId = -1;
            }
        }

        public static bool IsValid(string keyString, string separator = "-")
        {
            return (!keyString.StartsWith(separator) && keyString.Contains(separator));
        }

        public static ObjectKey TryCreate(QueryParser query)
        {
            ObjectKey key = new ObjectKey();
            int elementId = query.GetId(WebColumns.PageElementId);
            int pageId = query.GetId(WebColumns.PageId);

            if (elementId > 0)
            {
                key.ObjectId = WebObjects.WebPageElement;
                key.RecordId = elementId;
            }
            else if (pageId > 0)
            {
                key.ObjectId = WebObjects.WebPage;
                key.RecordId = pageId;
            }
            else
            {
                throw new Exception("");
            }

            return key;
        }

        public void Decode(int objectCode)
        {
            string stringCode = objectCode.ToString();

            ObjectId = DataHelper.GetId(stringCode.Substring(1, 3));
            RecordId = DataHelper.GetId(stringCode.Substring(4));
        }

        public int ToInteger()
        {
            return Convert.ToInt32(string.Format("1{0:000}{1}", ObjectId, RecordId));
        }

        public override string ToString()
        {
            return ToString('-');
        }

        public string ToString(char separator)
        {
            return string.Format("{0}{1}{2}", ObjectId, separator, RecordId);
        }

        public override int GetHashCode()
        {
            return ToInteger();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(IWebObject item)
        {
            return ObjectId == item.OBJECT_ID && RecordId == item.Id;
        }

        public bool HasValue
        {
            get { return this.RecordId > 0 && this.ObjectId > 0; }
        }

        //public static bool operator ==(ObjectKey key1, ObjectKey key2)
        //{
        //    return key1.ToInteger() == key2.ToInteger();
        //}

        //public static bool operator !=(ObjectKey key1, ObjectKey key2)
        //{
        //    return key1.ToInteger() != key2.ToInteger();
        //}
    }
}
