using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

namespace WCMS.Framework.Core
{
    public abstract class PageElementBase : PublicSecurableObject, IPageElement
    {
        #region IPageElement Members

        [ObjectColumn]
        public int Active { get; set; }

        public bool IsActive { get { return Active == 1; } }

        public abstract int MasterPageId { get; set; }

        public abstract WebMasterPage MasterPage { get; }

        [ObjectColumn]
        public int PartControlTemplateId
        {
            get { return _partControlTemplateId; }
            set
            {
                if (value != _partControlTemplateId)
                {
                    _template = null;
                    _partControlTemplateId = value;
                }
            }
        }
        private int _partControlTemplateId;


        [ObjectColumn]
        public int Rank { get; set; }

        //public abstract WebObjectContent ObjectContent { get; }

        [ObjectColumn]
        public int UsePartTemplatePath { get; set; }

        public abstract WSite Site { get; }

        private WebPartControlTemplate _template = null;
        public WebPartControlTemplate PartControlTemplate
        {
            get
            {
                if (_template == null)
                    _template = WebPartControlTemplate.Get(PartControlTemplateId);
                return _template;
            }
        }

        public abstract WebTemplatePanel Panel { get; }

        public abstract int TemplatePanelId { get; set; }

        public abstract WPage Page { get; }

        public PageElementBase LocatePair(string keyString)
        {
            var thisPage = Page;
            var pages = Site.Pages;
            var parameter = GetParameter(keyString + "_Key");

            if (parameter != null)
            {
                ObjectKey objectKey = new ObjectKey(parameter.Value);

                //string[] pairTarget = parameter.Value.Split(',');
                //int objectId = DataHelper.GetDbId(pairTarget.First());
                //int recordId = DataHelper.GetDbId(pairTarget[1]);

                if (objectKey.HasValue)
                {
                    if (objectKey.ObjectId == WebObjects.WebPage)
                        return WPage.Get(objectKey.RecordId);
                    else if (objectKey.ObjectId == WebObjects.WebPageElement)
                        return WebPageElement.Get(objectKey.RecordId);
                    else
                        throw new Exception("Pairing object is not a WebPage nor a PageElement: " + objectKey);
                }
            }

            return null;
        }

        #endregion
    }
}
