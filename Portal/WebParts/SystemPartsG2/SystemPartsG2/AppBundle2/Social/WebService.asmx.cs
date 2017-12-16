using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using WCMS.Framework.Core;
using WCMS.Framework.Social;


namespace WCMS.WebSystem.WebParts.Social
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SocialWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool DeleteWallEntry(int id)
        {
            if (id > 0)
            {
                WallUpdate item = WallUpdate.Provider.Get(id);
                if (item != null)
                {
                    try
                    {
                        // Delete comments first
                        var comments = WebComment.Provider.GetList(-2, item.OBJECT_ID, item.Id, -2);
                        if (comments.Count() > 0)
                        {
                            foreach (var comment in comments)
                                comment.Delete();
                        }
                    }
                    catch { }

                    return item.Delete();
                }
            }

            return false;
        }
    }
}
