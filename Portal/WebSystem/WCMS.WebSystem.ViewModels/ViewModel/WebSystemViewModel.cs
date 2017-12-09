using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.WebSystem.Controls;
using WCMS.Framework;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.ViewModel
{
    public class WebSystemViewModel
    {
        public static void BuildGenericTabs(int objectId, int recordId, WQuery query, ITabControl tabControl)
        {
            // ObjectKey
            var q = query.Clone();
            q.Set(ObjectKey.KeyString, new ObjectKey(objectId, recordId));

            var fileName = query.BasePath;
            if (!DataHelper.IsPresent(fileName, new string[] { CentralPages.WebParameters, CentralPages.WebResources, CentralPages.WebSecurity }))
                q.Set(ObjectKey.KeySource, query.EncodedBasePath);

            tabControl.AddTab("tabSecurity", "Security", q.BuildQuery(CentralPages.WebSecurity), CentralPages.WebSecurity);
            tabControl.AddTab("tabResources", "Resources", q.BuildQuery(CentralPages.WebResources), CentralPages.WebResources);
            tabControl.AddTab("tabParameters", "Parameters", q.BuildQuery(CentralPages.WebParameters), CentralPages.WebParameters);
        }
    }
}
