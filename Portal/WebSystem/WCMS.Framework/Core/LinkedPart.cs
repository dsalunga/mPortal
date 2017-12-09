using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public class LinkedPart
    {
        public LinkedPart()
        {
            PartConfigId = -1;
            LinkedPartControlId = -1;
            TargetObjectId = -1;
        }

        public int PartConfigId { get; set; }
        public int LinkedPartControlId { get; set; }
        public int TargetObjectId { get; set; }

        public string TargetObjectName
        {
            get
            {
                if (TargetObjectId == WebObjects.WebPage)
                    return "Page";
                else if (TargetObjectId == WebObjects.WebPageElement)
                    return "Page Element";
                else
                    return "Page & Page Elements";
            }
        }

        public WebPartConfig PartConfig
        {
            get
            {
                if (PartConfigId > 0)
                {
                    return WebPartConfig.Get(PartConfigId);
                }

                return null;
            }
        }

        public WebPartConfig LinkedConfig
        {
            get
            {
                if (LinkedPartControlId > 0)
                {
                    return WebPartConfig.Get(LinkedPartControlId);
                }

                return null;
            }
        }
    }
}
