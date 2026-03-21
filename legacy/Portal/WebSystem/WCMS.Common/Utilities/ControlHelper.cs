using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ControlHelper
/// </summary>
namespace WCMS.Common.Utilities
{
    public enum LinkType : int
    {
        FullLink = 1,
        Page = 2
    }

    public class ControlHelper : NameValueCollection
    {
        private string _PageType;
        private int _ItemID;
        private int _SiteID;
        private bool _IsValid;
        //private NameValueCollection _nvcSettings;

        public ControlHelper()
        {

        }

        public ControlHelper(NameValueCollection nvcQueryString)
        {
            //_nvcSettings = nvcQueryString;
            //_nvcSettings = new NameValueCollection();
            this.Add(nvcQueryString);
        }

        //public ControlHelper(string strSettings)
        //{
        //    CHProcess(strSettings);
        //}

        //public ControlHelper(Control ctl)
        //{
        //    CHProcess(((Literal)ctl.FindControl("___LiteralID")).Text);
        //}

        //public void CHProcess(string strSettings)
        //{
        //    //_nvcSettings = new NameValueCollection();
        //    string[] strKeys = strSettings.Split('&');
        //    foreach (string strKey in strKeys)
        //    {
        //        string[] str = strKey.Split('=');
        //        try
        //        {
        //            this.Add(str[0], str[1]);
        //        }
        //        catch { }
        //    }

        //    try
        //    {
        //        if (string.IsNullOrEmpty(this["ID"]))
        //        {
        //            _SiteID = int.Parse(this[WebColumns.SiteId]);
        //            _PageType = this["PageType"];
        //            _ItemID = int.Parse(this["SitePageItemID"]);
        //        }
        //        else
        //        {
        //            _SiteID = int.Parse(this["S"]);
        //            string[] strElement = this["ID"].Split(',');
        //            _PageType = strElement[0];
        //            _ItemID = int.Parse(strElement[1]);
        //        }
        //        _IsValid = true;
        //    }
        //    catch
        //    {
        //        _IsValid = false;
        //    }
        //}

        //public string ToLink(string strValue, LinkType lnkType)
        //{
        //    if (strValue != null)
        //    {
        //        switch (lnkType)
        //        {
        //            case LinkType.FullLink:
        //                this["SS"] = strValue;
        //                break;
        //            case LinkType.Page:
        //                this["ShowP"] = strValue;
        //                break;
        //            default:
        //                this["SS"] = strValue;
        //                break;
        //        }
        //    }
        //    string str = string.Empty;
        //    foreach (string strKey in this.AllKeys)
        //    {
        //        str += strKey + "=" + this[strKey] + "&";
        //    }
        //    return str.TrimEnd('&');

        //}

        public string ToLink()
        {
            string str = string.Empty;
            foreach (string strKey in this.AllKeys)
            {
                str += strKey + "=" + this[strKey] + "&";
            }
            return str.TrimEnd('&');
        }

        public NameValueCollection Settings
        {
            /*
            set
            {
                _nvcSettings = value;
            }
            */

            get
            {
                return this;
            }
        }

        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
        }

        public string PageType
        {
            get
            {
                return _PageType;
            }
        }

        public int ItemID
        {
            get
            {
                return _ItemID;
            }
        }

        public int SiteID
        {
            get
            {
                return _SiteID;
            }
        }
    }
}
