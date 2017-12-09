using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WContext : WebContextBase
    {
        private const string KEY = "WContext";

        private Control _control;
        private int _internalPageId = -1;
        private int _contextType = -1;

        private bool _razor = false;

        //private int _pageId = -1;
        //private int _elementId = -1;
        //private int _elementRecordId = -1;
        //private int _elementObjectId = -1;

        public static WContext GetInstance(dynamic args = null)
        {
            //var context = HttpContext.Current;
            //if (context.Items.Contains(KEY))
            //    return context.Items[KEY] as WContext;

            if (args == null)
                args = HttpContext.Current;

            var item = new WContext(args);
            //context.Items[KEY] = item;
            return item;
        }

        #region Constructors

        public WContext()
        {
            RecordId = -1;
            ObjectId = -1;

            PartAdminId = -1;
            PartControlId = -1;
        }

        public WContext(Control userControl)
            : this()
        {
            _control = userControl;
            Query = new WQuery(userControl);

            this.Initialize(userControl.ID);
        }

        public WContext(System.Web.WebPages.WebPageBase page, IPageElement element = null)
            : this()
        {
            Query = new WQuery(page.Request);
            _razor = true;

            var e = element == null ? page.PageData["Element"] ?? page.PageData["Page"] : element;
            if (e != null)
                this.Initialize(e);
            else
                this.Initialize();
        }

        public WContext(HttpRequest request)
            : this()
        {
            Query = new WQuery(request);
            this.Initialize();
        }

        public WContext(HttpContext context)
            : this()
        {
            Query = new WQuery(context);
            this.Initialize();
        }

        #endregion

        #region Properties

        public WSession Session
        {
            get { return WSession.Current; }
        }

        public int ContextType
        {
            get { return _contextType; }
        }

        public HttpContext HttpContext { get { return HttpContext.Current; } }

        public int RecordId { get; set; }
        public int ObjectId { get; set; }

        public int PartControlId { get; set; }
        public int PartAdminId { get; set; }

        public WQuery Query { get; set; }
        public Control Control { get { return _control; } }

        public PageElementBase Element
        {
            get
            {
                if (ObjectId == WebObjects.WebPage)
                    return WPage.Get(RecordId);
                else if (ObjectId == WebObjects.WebPageElement)
                    return WebPageElement.Get(RecordId);
                else if (ObjectId == -1 && PageId > 0)
                    return WPage.Get(PageId);
                
                //throw new Exception("Not a Page nor a PageElement");
                return null;
            }
        }

        /// <summary>
        /// Returns the main object involved in the context. i.e. Page, Element or PartAdmin
        /// </summary>
        public ParameterizedWebObject ParameterizedObject
        {
            get
            {
                if (ObjectId == WebObjects.WebPage)
                    return WPage.Get(RecordId);
                else if (ObjectId == WebObjects.WebPageElement)
                    return WebPageElement.Get(RecordId);
                //else if ((_elementId = Query.GetId(WebColumns.PageElementId)) > 0)
                //    return WebPageElement.Get(_elementId);
                //else if ((_pageId = Query.GetId(WebColumns.PageId)) > 0)
                //    return WebPage.Get(_pageId);
                else if (PartAdminId > 0)
                    return PartAdmin;
                else
                    throw new Exception("Not a Page nor a PageElement");
            }
        }

        public WebPartAdmin PartAdmin
        {
            get { return PartAdminId > 0 ? WebPartAdmin.Get(PartAdminId) : null; }
        }

        public WebPartControl PartControl
        {
            get { return PartControlId > 0 ? WebPartControl.Get(PartControlId) : null; }
        }

        public string BasePath
        {
            get { return Query.BasePath; }
            set { Query.BasePath = value; }
        }

        /// <summary>
        /// IP Address of the client
        /// </summary>
        public string UserHostAddress
        {
            get { return HttpContext.Request.UserHostAddress; }
        }

        //public string this[string name]
        //{
        //    get { return Query[name]; }
        //    set { Query[name] = value; }
        //}

        /// <summary>
        /// What is the purpose of this?
        /// </summary>
        public int PageId
        {
            get
            {
                if (_internalPageId == -1)
                {
                    _internalPageId = DataHelper.GetId(Context.Request, WebColumns.PageIdInternal);
                    if (_internalPageId == -1 && !_razor)
                    {
                        var hidden = _control.Page.Form.FindControl(WConstants.HIDDEN_PAGE_ID) as HtmlInputHidden;
                        if (hidden != null)
                            _internalPageId = DataHelper.GetId(hidden.Value);
                    }
                }

                return _internalPageId;
            }
        }

        public WPage Page
        {
            get { return WPage.Get(PageId); }
        }

        public WSite Site
        {
            get
            {
                var page = Page;
                if (page != null)
                    return page.Site;

                return null;
            }
        }

        private Dictionary<string, INamedValueProvider> _valueProvider;
        public Dictionary<string, INamedValueProvider> ValueProvider
        {
            get
            {
                if (_valueProvider == null)
                {
                    var page = Page;
                    _valueProvider = new Dictionary<string, INamedValueProvider>();
                    _valueProvider.Add("Page", page);
                    _valueProvider.Add("Site", page.Site);
                    _valueProvider.Add("MasterPage", page.MasterPage);

                    if (ObjectId == WebObjects.WebPageElement)
                        _valueProvider.Add("Element", WebPageElement.Get(RecordId));
                    else if (ObjectId == WebObjects.WebPage)
                        _valueProvider.Add("Element", page);
                }

                return _valueProvider;
            }
        }

        #endregion

        #region Static Methods

        public static string GenerateControlId(int objectId, int recordId)
        {
            string id = string.Format("O{0:000}R{1}", objectId, recordId);

            return id;
        }

        public static string GenerateControlId(IWebObject item)
        {
            return GenerateControlId(item.OBJECT_ID, item.Id);
        }

        public static void StaticRedirect()
        {
            WQuery.StaticRedirect();
        }

        public static void StaticRedirect(string destination)
        {
            WQuery.StaticRedirect(destination);
        }

        public static void StaticRedirect(string addressOrBasePath, bool retainParameters)
        {
            WQuery.StaticRedirect(addressOrBasePath, retainParameters);
        }

        public static Control GetParent(Control control)
        {
            Control parent = control;

            do
            {
                parent = parent.Parent;
            }
            while (!(parent != null && parent.ID != null && parent.ID.Length > 5 && parent.ID.StartsWith("O") && parent.ID.Substring(4, 1) == "R"));

            return parent;
        }

        #endregion

        #region Instance Methods

        public bool IsUserMgmtPermitted(int permissionId)
        {
            return Element.IsUserMgmtPermitted(permissionId);
        }

        public string this[string key]
        {
            get { return Query[key]; }
            set { Query[key] = value; }
        }

        private void Initialize(IPageElement element)
        {
            Initialize(element.OBJECT_ID, element.Id);
        }

        private void Initialize(int objectId, int recordId)
        {
            if (objectId > 0)
            {
                ObjectId = objectId;
                RecordId = recordId;
                if (ObjectId == WebObjects.WebPartControl || ObjectId == WebObjects.WebPartAdmin)
                {
                    // EditMode or AdminMode

                    //_internalPageId = Query.GetId(WebColumns.PageId);
                    _contextType = ObjectId == WebObjects.WebPartControl ? WContextTypes.EditMode : WContextTypes.AdminMode;
                    if (ObjectId == WebObjects.WebPartControl)
                        PartControlId = RecordId;
                    else
                        PartAdminId = RecordId;

                    SetUpContextFromQuery();
                }
                else
                {
                    // FrontEnd
                    _contextType = WContextTypes.FrontEnd;
                }
            }
            else
            {
                // EditMode
                SetUpContextFromQuery();
            }
        }

        private void Initialize(string id = "")
        {
            var objectId = -1;
            var recordId = -1;

            if (!string.IsNullOrEmpty(id) && id[0] == 'O')
            {
                objectId = DataHelper.GetId(id.Substring(1, 3));
                recordId = DataHelper.GetId(id.Substring(5));
            }

            Initialize(objectId, recordId);
        }

        private void SetUpContextFromQuery()
        {
            int pageElementId = Query.GetId(WebColumns.PageElementId);
            int pageId = Query.GetId(WebColumns.PageId);
            int adminId = Query.GetId(WebColumns.PartAdminId);
            if (pageElementId > 0)
            {
                RecordId = pageElementId;
                ObjectId = WebObjects.WebPageElement;
                _contextType = WContextTypes.EditMode;
            }
            else if (pageId > 0)
            {
                RecordId = pageId;
                ObjectId = WebObjects.WebPage;
                _contextType = WContextTypes.EditMode;
            }
            else if (adminId > 0)
            {
                RecordId = adminId;
                ObjectId = WebObjects.WebPartAdmin;
                PartAdminId = adminId;
                _contextType = WContextTypes.AdminMode;
            }

            _internalPageId = pageId;
        }

        public void Redirect()
        {
            Query.Redirect();
        }

        public void Redirect(string basePath)
        {
            Query.Redirect(basePath);
        }

        public void SetSourceAndRedirect(string basePath)
        {
            Query.SetSourceAndRedirect(basePath);
        }

        public void SetLoad(string partConfigFile)
        {
            Query[WConstants.Load] = partConfigFile;
        }

        public void SetLoadAndRedirect(string partConfigFile)
        {
            SetLoad(partConfigFile);
            this.Redirect();
        }

        public void Remove(string name)
        {
            Query.Remove(name);
        }

        public string BuildQuery()
        {
            return Query.BuildQuery();
        }

        public string Get(string key)
        {
            return Query.Get(key);
        }

        public string GetSource()
        {
            return Query.GetSource();
        }

        public string GetDecode(string key)
        {
            return Query.GetDecode(key);
        }

        public int GetId(string key)
        {
            return Query.GetId(key);
        }

        public int GetInt32(string key)
        {
            return Query.GetInt32(key);
        }

        public int GetInt32(string key, int defaultValue)
        {
            return Query.GetInt32(key, defaultValue);
        }

        public override string ToString()
        {
            return Query.ToString();
        }

        public WContext Set(string name, object value)
        {
            Query[name] = value.ToString();
            return this;
        }

        public WContext SetEncode(string name, object value)
        {
            Query.SetEncode(name, value.ToString());
            return this;
        }

        /// <summary>
        /// Sets which webpart to load (in relation to the configured part for current element). This is used only in front-end. 
        /// In Admin, Load parameter is the one required.
        /// </summary>
        /// <param name="value"></param>
        public WContext SetOpen(object value)
        {
            Query[WConstants.Open] = value.ToString();
            return this;
        }

        public string GetOpen()
        {
            return Query.Get(WConstants.Open);
        }

        public WContext RemoveOpen()
        {
            Query.Remove(WConstants.Open);
            return this;
        }

        /// <summary>
        /// Sets Open parameter and Redirects the page.
        /// </summary>
        /// <param name="value"></param>
        public void Open(object value)
        {
            SetOpen(value);
            Redirect();
        }


        /// <summary>
        /// Removes Open parameter and Redirects the page.
        /// </summary>
        public void Open()
        {
            Remove(WConstants.Open);
            Redirect();
        }

        public WContext SetSource(string value)
        {
            Query.SetSource(value);
            return this;
        }

        #endregion

        #region Some Utilities

        public ParameterizedWebObject GetParameterSet(string key)
        {
            var paramSetName = this.Element.GetParameterValue(key);
            WebParameterSet paramSet = !string.IsNullOrEmpty(paramSetName) ? WebParameterSet.Get(paramSetName) : null;
            return paramSet;
        }

        /// <summary>
        /// Calls GetChild() with no fallback. Returns the default set "ParameterSet".
        /// </summary>
        /// <returns></returns>
        public ParameterizedWebObject GetParameterSet()
        {
            return Element.GetParameterSet();
        }

        #endregion
    }
}
