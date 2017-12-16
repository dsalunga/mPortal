using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WCMS.WebSystem.WebParts.RemoteIndexer;

namespace WCMS.WebSystem.WebParts.FileManager
{
    /// <summary>
    /// Summary description for Indexer
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IndexerView : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public RemoteLibrary GetLibrary(int id)
        {
            return RemoteLibrary.Provider.Get(id);
        }

        [WebMethod]
        public List<RemoteLibrary> GetLibraries()
        {
            return RemoteLibrary.Provider.GetList().ToList();
        }

        [WebMethod]
        public RemoteItem GetItem(int id)
        {
            return RemoteItem.Provider.Get(id);
        }

        [WebMethod]
        public List<RemoteItem> GetItems(int libraryId, int parentId = -2)
        {
            return RemoteItem.Provider.GetList(libraryId, parentId).ToList();
        }
    }
}
