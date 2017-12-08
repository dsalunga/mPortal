using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using WCMS.BibleReader.Core;

namespace WCMS.BibleReader
{
    /// <summary>
    /// Summary description for BibleService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BibleService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetVerseContent(string versionShortName, int bookCode, int chapterCode, int verseCode)
        {
            return BibleManager.GetVerseContent(versionShortName, bookCode, chapterCode, verseCode);
        }

        [WebMethod]
        public BibleVerse GetVerse(string versionShortName, int bookCode, int chapterCode, int verseCode)
        {
            return BibleManager.GetVerse(versionShortName, bookCode, chapterCode, verseCode);
        }
    }
}
