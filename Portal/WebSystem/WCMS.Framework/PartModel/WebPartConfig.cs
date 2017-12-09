using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using WCMS.Framework.Core;

namespace WCMS.Framework
{
    [Obsolete]
    public class WebPartConfig : IWebObject
    {
        private static IWebPartConfigProvider _provider = DataAccess.CreateProvider<IWebPartConfigProvider>();

        private int _partConfigId = -1;
        /// <summary>
        /// PartConfigId
        /// </summary>
        public int Id
        {
            get { return _partConfigId; }
            set { _partConfigId = value; }
        }

        private int _partId = -1;
        public int PartId
        {
            get { return _partId; }
            set { _partId = value; }
        }

        public string Name { get; set; }
        public string FileName { get; set; }

        public WPart Part
        {
            get
            {
                if (PartId > 0)
                    return WPart.Get(PartId);

                return null;
            }
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        //public static IEnumerable<WebPartConfig> GetList()
        //{
        //    return _provider.Get();
        //}

        public static IEnumerable<WebPartConfig> GetList(int partId)
        {
            return _provider.GetList(partId);
        }

        public static WebPartConfig Get(int partConfigId)
        {
            return _provider.Get(partConfigId);
        }

        public static bool Delete(int partConfigId)
        {
            return _provider.Delete(partConfigId);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return WebObjects.WebPartConfig; }
        }

        #endregion
    }
}
