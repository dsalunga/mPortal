using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WContext : WebContextBase, IWContext
    {
        private const string KEY = "WContext";
        public const string CurrentElementItemKey = "WContext.CurrentElement";
        public const string CurrentPageItemKey = "WContext.CurrentPage";

        private int _internalPageId = -1;
        private int _contextType = -1;

        //private int _pageId = -1;
        //private int _elementId = -1;
        //private int _elementRecordId = -1;
        //private int _elementObjectId = -1;

        public static WContext GetInstance(dynamic args = null)
        {
            if (args == null)
                args = HttpContextHelper.Current;

            var item = new WContext(args);
            return item;
        }

        #region Constructors

        public WContext()
        {
            RecordId = -1;
            ObjectId = -1;

            PartAdminId = -1;
            PartControlId = -1;

            Query = CreateInitialQuery();
            Initialize();
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

        public WContext(HttpRequest request, IPageElement element)
            : this()
        {
            Query = new WQuery(request);
            if (element != null)
                this.Initialize(element);
            else
                this.Initialize();
        }

        #endregion

        #region Properties

        public WSession Session
        {
            get { return WSession.Current; }
        }

        IWSession IWContext.Session
        {
            get { return Session; }
        }

        public int ContextType
        {
            get { return _contextType; }
        }

        public HttpContext HttpContext { get { return HttpContextHelper.Current; } }

        public int RecordId { get; set; }
        public int ObjectId { get; set; }

        public int PartControlId { get; set; }
        public int PartAdminId { get; set; }

        public WQuery Query { get; set; }

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
            get { return EnsureQuery().BasePath; }
            set { EnsureQuery().BasePath = value; }
        }

        /// <summary>
        /// IP Address of the client
        /// </summary>
        public string UserHostAddress
        {
            get { return HttpContext.Connection.RemoteIpAddress?.ToString(); }
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
                    _internalPageId = DataUtil.GetId(Context.Request, WebColumns.PageIdInternal);
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
            QueryParser.StaticRedirect();
        }

        public static void StaticRedirect(string destination)
        {
            QueryParser.StaticRedirect(destination);
        }

        public static void StaticRedirect(string addressOrBasePath, bool retainParameters)
        {
            QueryParser.StaticRedirect(addressOrBasePath, retainParameters);
        }


        #endregion

        #region Instance Methods

        public bool IsUserMgmtPermitted(int permissionId)
        {
            return Element.IsUserMgmtPermitted(permissionId);
        }

        public string this[string key]
        {
            get { return EnsureQuery()[key]; }
            set { EnsureQuery()[key] = value; }
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
                objectId = DataUtil.GetId(id.Substring(1, 3));
                recordId = DataUtil.GetId(id.Substring(5));
            }

            Initialize(objectId, recordId);
        }

        private void SetUpContextFromQuery()
        {
            var query = EnsureQuery();
            int pageElementId = query.GetId(WebColumns.PageElementId);
            int pageId = query.GetId(WebColumns.PageId);
            int adminId = query.GetId(WebColumns.PartAdminId);
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
            else
            {
                var httpContext = HttpContextHelper.Current;
                if (httpContext?.Items != null)
                {
                    if (httpContext.Items.TryGetValue(CurrentElementItemKey, out var elementObj) &&
                        elementObj is IPageElement currentElement)
                    {
                        RecordId = currentElement.Id;
                        ObjectId = currentElement.OBJECT_ID;
                        _contextType = WContextTypes.FrontEnd;

                        if (currentElement is WPage currentPage)
                        {
                            _internalPageId = currentPage.Id;
                        }
                        else if (currentElement is WebPageElement currentPageElement)
                        {
                            try
                            {
                                _internalPageId = currentPageElement.Page?.Id ?? -1;
                            }
                            catch
                            {
                                _internalPageId = -1;
                            }
                        }

                        return;
                    }

                    if (httpContext.Items.TryGetValue(CurrentPageItemKey, out var pageObj) &&
                        pageObj is WPage currentPageFromItems)
                    {
                        RecordId = currentPageFromItems.Id;
                        ObjectId = WebObjects.WebPage;
                        _contextType = WContextTypes.FrontEnd;
                        _internalPageId = currentPageFromItems.Id;
                        return;
                    }
                }
            }

            _internalPageId = pageId;
        }

        public void Redirect()
        {
            EnsureQuery().Redirect();
        }

        public void Redirect(string basePath)
        {
            EnsureQuery().Redirect(basePath);
        }

        public void SetSourceAndRedirect(string basePath)
        {
            EnsureQuery().SetSourceAndRedirect(basePath);
        }

        public void SetLoad(string partConfigFile)
        {
            EnsureQuery()[WConstants.Load] = partConfigFile;
        }

        public void SetLoadAndRedirect(string partConfigFile)
        {
            SetLoad(partConfigFile);
            this.Redirect();
        }

        public void Remove(string name)
        {
            EnsureQuery().Remove(name);
        }

        public string BuildQuery()
        {
            return EnsureQuery().BuildQuery();
        }

        public string Get(string key)
        {
            return EnsureQuery().Get(key);
        }

        public string GetSource()
        {
            return EnsureQuery().GetSource();
        }

        public string GetDecode(string key)
        {
            return EnsureQuery().GetDecode(key);
        }

        public int GetId(string key)
        {
            return EnsureQuery().GetId(key);
        }

        public int GetInt32(string key)
        {
            return EnsureQuery().GetInt32(key);
        }

        public int GetInt32(string key, int defaultValue)
        {
            return EnsureQuery().GetInt32(key, defaultValue);
        }

        public override string ToString()
        {
            return EnsureQuery().ToString();
        }

        public WContext Set(string name, object value)
        {
            EnsureQuery()[name] = value.ToString();
            return this;
        }

        public WContext SetEncode(string name, object value)
        {
            EnsureQuery().SetEncode(name, value.ToString());
            return this;
        }

        /// <summary>
        /// Sets which webpart to load (in relation to the configured part for current element). This is used only in front-end. 
        /// In Admin, Load parameter is the one required.
        /// </summary>
        /// <param name="value"></param>
        public WContext SetOpen(object value)
        {
            EnsureQuery()[WConstants.Open] = value.ToString();
            return this;
        }

        public string GetOpen()
        {
            return EnsureQuery().Get(WConstants.Open);
        }

        public WContext RemoveOpen()
        {
            EnsureQuery().Remove(WConstants.Open);
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
            EnsureQuery().SetSource(value);
            return this;
        }

        private static WQuery CreateInitialQuery()
        {
            var currentHttpContext = HttpContextHelper.Current;
            if (currentHttpContext != null)
                return new WQuery(currentHttpContext);

            return new WQuery();
        }

        private WQuery EnsureQuery()
        {
            if (Query == null)
                Query = CreateInitialQuery();

            return Query;
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
