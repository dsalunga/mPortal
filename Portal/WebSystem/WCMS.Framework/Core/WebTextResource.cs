using System;
using System.Linq;
using System.Collections.Generic;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public class WebTextResource : WebObjectBase, ISelfManager
    {
        private static IWebTextResourceProvider _provider;

        static WebTextResource()
        {
            _provider = WebObject.ResolveManager<WebTextResource,
                IWebTextResourceProvider>(WebObject.ResolveProvider<WebTextResource, IWebTextResourceProvider>());
        }

        public WebTextResource()
        {
            DirectoryId = -1;
            ContentTypeId = -1;
            Rank = 0;
            PhysicalPath = string.Empty;
        }

        #region Properties

        [ObjectColumn]
        public int ContentTypeId { get; set; }

        [ObjectColumn]
        public string Title { get; set; }

        [ObjectColumn]
        public string Content { get; set; }

        [ObjectColumn]
        public int DirectoryId { get; set; }

        [ObjectColumn]
        public int Rank { get; set; }

        [ObjectColumn]
        public DateTime DateModified { get; set; }

        [ObjectColumn]
        public string PhysicalPath { get; set; }

        /// <summary>
        /// Utc
        /// </summary>
        [ObjectColumn]
        public DateTime DatePersisted { get; set; }

        public WebConstant ContentType
        {
            get
            {
                if (ContentTypeId > 0)
                    return WebConstant.Provider.Get(ContentTypeId);

                return null;
            }
        }

        public string Permalink
        {
            get
            {
                return string.Format(WConstants.ResourcePermalinkFormat,
                            this.Id, this.DateModified.ToString("yyyyMMddHHmmss"), this.Title.Replace(" ", "_"));
            }
        }

        public string BuildRelativePhysicalPath()
        {
            if (string.IsNullOrEmpty(PhysicalPath) || !PhysicalPath.StartsWith("~"))
            {
                string resFile = string.Format("{0}_{1}", this.Id, this.Title);
                return FileHelper.Combine(WConstants.ResourcePhysicalPathStart, resFile, '/');
            }

            return PhysicalPath;
        }

        public string BuildAbsolutePhysicalPath()
        {
            return WebHelper.MapPath(BuildRelativePhysicalPath());
        }

        #endregion

        public int Update()
        {
            return _provider.Update(this);
        }

        public string RenderAsText(bool externalMode)
        {
            if (externalMode)
            {
                switch (this.ContentType.Value)
                {
                    case "text/css":
                        return string.Format(@"<link rel=""Stylesheet"" type=""text/css"" href=""{0}"" />", this.Permalink);

                    case "application/javascript":
                        return string.Format(@"<script src=""{0}"" type=""text/javascript""></script>", this.Permalink);

                    default:
                        return string.Format("{0}<!-- {1} -->{0}{2}{0}", Environment.NewLine, this.Title, this.Content);
                }
            }
            else
            {
                switch (this.ContentType.Value)
                {
                    case "text/css":
                        return string.Format("<style type=\"text/css\" media=\"all\">{0}/* {2} */{0}{1}{0}</style>",
                            Environment.NewLine, this.Content, this.Title);

                    case "application/javascript":
                        return string.Format("<script language=\"javascript\" type=\"text/javascript\">{0}/* {2} */{0}{1}{0}</script>",
                            Environment.NewLine, this.Content, this.Title);

                    default:
                        return string.Format("{0}<!-- {1} -->{0}{2}{0}", Environment.NewLine, this.Title, this.Content);
                }
            }
        }

        #region Static Methods & Properties

        public static IWebTextResourceProvider Provider { get { return _provider; } }

        public static IEnumerable<WebConstant> GetContentTypes()
        {
            return WebConstant.Provider.GetList(WConstants.MIME);
        }

        public static IEnumerable<WebTextResource> GetList()
        {
            return _provider.GetList();
        }

        public static WebTextResource Get(int textResourceId)
        {
            return _provider.Get(textResourceId);
        }

        public static IEnumerable<WebTextResource> GetByDirectory(int directoryId)
        {
            return _provider.GetByDirectory(directoryId);
        }

        public static bool Delete(int id)
        {
            var links = WebObjectHeader.GetList(id);
            for (int i = 0; i < links.Count(); i++)
                links.ElementAt(i).Delete();

            return _provider.Delete(id);
        }

        #endregion


        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebTextResource; }
        }

        #endregion

        #region ISelfManager Members

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion
    }
}
