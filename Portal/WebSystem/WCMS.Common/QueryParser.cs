using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.IO;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Summary description for QueryStringParser.
    /// </summary>
    public class QueryParser : NameValueCollection
    {
        private const string PageIdInternal = "___pid";
        public const string SourceKey = "RequestUrl";

        /// <summary>
        /// Encode only on Get, not on Set
        /// </summary>
        //public static string ReturnKey = SourceKey;  //"ReturnUrl";
        //public const string LoadKey = "Load";

        #region Constructors

        public QueryParser() { }

        public QueryParser(NameValueCollection queryString)
        {
            base.Add(queryString);
        }

        public QueryParser(QueryParser toCopy)
        {
            this.BasePath = toCopy.BasePath;
            this.Add(toCopy);
        }

        public QueryParser(bool useHttpContext)
        {
            if (useHttpContext)
                Init(HttpContext.Current.Request);
        }

        public void Init(HttpRequest request)
        {
            string pageId = request[PageIdInternal];
            string requestPath = request.AppRelativeCurrentExecutionFilePath;
            if (pageId != null && (requestPath.Equals("~/Default.aspx") || requestPath.Equals("~/Static.aspx")))
            {
                int qIndex = request.RawUrl.IndexOf("?");
                BasePath = qIndex > 0 ? request.RawUrl.Substring(0, qIndex) : request.RawUrl;
                Add(request.QueryString);

                // For further checking...
                // When the page has been rewritten but still containing a pageId then...
                Remove(PageIdInternal);
            }
            else
            {
                BasePath = request.CurrentExecutionFilePath;
                Add(request.QueryString);
            }
        }

        public void Init(HttpRequestBase request)
        {
            string pageId = request[PageIdInternal];
            string requestPath = request.AppRelativeCurrentExecutionFilePath;
            if (pageId != null && (requestPath.Equals("~/Default.cshtml")))
            {
                int qIndex = request.RawUrl.IndexOf("?");
                BasePath = qIndex > 0 ? request.RawUrl.Substring(0, qIndex) : request.RawUrl;
                Add(request.QueryString);

                // For further checking...
                // When the page has been rewritten but still containing a pageId then...
                Remove(PageIdInternal);
            }
            else
            {
                BasePath = request.CurrentExecutionFilePath;
                Add(request.QueryString);
            }
        }

        public QueryParser(HttpRequest request)
        {
            Init(request);
        }

        public QueryParser(HttpRequestBase request)
        {
            Init(request);
        }

        public QueryParser(System.Web.WebPages.WebPage page) : this(page.Request) { }

        public QueryParser(Page p)
            : this(p.Request) { }

        public QueryParser(UserControl c)
            : this(c.Request) { }

        public QueryParser(Control c)
            : this(c.Page.Request) { }

        public QueryParser(HttpApplication a)
            : this(a.Request) { }

        public QueryParser(HttpContext c)
            : this(c.Request) { }

        public QueryParser(string queryStringOrPath)
        {
            string queryString = null;

            if (queryStringOrPath.Contains(":"))
            {
                var pathSlash = queryStringOrPath.IndexOf("/", 10);
                if(pathSlash > 0){
                    BaseAddress = queryStringOrPath.Substring(0, pathSlash);
                    queryStringOrPath = queryStringOrPath.Substring(pathSlash);
                }else{
                    BaseAddress = queryStringOrPath;
                    BasePath = "/";
                    return;
                }
            }

            if (queryStringOrPath.Contains("?"))
            {
                string[] path = queryStringOrPath.Split('?');
                BasePath = path[0];
                queryString = path[1];
            }
            else if (queryStringOrPath.Contains("="))
            {
                // all query string
                queryString = queryStringOrPath;
            }
            else if (!string.IsNullOrEmpty(queryStringOrPath))
            {
                BasePath = queryStringOrPath;
            }

            if (!string.IsNullOrEmpty(queryString))
                AddParams(queryString);
        }

        #endregion

        #region Properties

        private string _basePath;
        /// <summary>
        /// The path starting from /
        /// </summary>
        public string BasePath
        {
            get { return _basePath; }
            set { _basePath = value; }
        }

        /// <summary>
        /// DOMAIN:PORT_NUMBER
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Returns true if SourceKey has a value
        /// </summary>
        public bool HasSourceValue
        {
            get { return HasValue(SourceKey); }
        }

        public bool HasValue(string key)
        {
            string value = this[key];
            return !string.IsNullOrEmpty(value);
        }

        public string EncodedBasePath
        {
            get { return HttpUtility.UrlEncode(_basePath); }
        }

        public string BaseFileName
        {
            get { return Path.GetFileName(BasePath); }
        }

        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        #endregion

        #region Instance Methods

        private void BaseRedirect(string path)
        {
            WebHelper.Redirect(path, Context);
        }

        public void AddParams(string queryString)
        {
            base.Add(HttpUtility.ParseQueryString(queryString));
        }

        public string Get(string key, string nullEmptyDefaultValue)
        {
            return GetValue(key, nullEmptyDefaultValue);
        }

        private string _ToString()
        {
            string queryString = string.Empty;
            foreach (string key in base.AllKeys)
            {
                queryString += key + "=" + base[key] + "&";
            }

            return queryString.TrimEnd(new char[] { '&', '=', ' ', '?' });
        }

        public string BuildQuery(bool absolute = false)
        {
            return BuildQuery(BasePath, absolute);
        }


        public override string ToString()
        {
            return BuildQuery();
        }

        public void BuildBaseAddress()
        {
            if (string.IsNullOrEmpty(BaseAddress))
            {
                var url = Context.Request.Url;
                BaseAddress = url.Scheme + "://" + url.Authority;
            }
        }

        public string BuildQuery(string basePath, bool absolute = false)
        {
            string queryString = _ToString();
            if (!string.IsNullOrEmpty(queryString))
                queryString = "?" + queryString;

            if (absolute)
                BuildBaseAddress();

            if (!absolute || string.IsNullOrEmpty(BaseAddress))
                return basePath + queryString;
            else
                return WebHelper.CombineAddress(BaseAddress, basePath) + queryString;
        }

        public void Redirect()
        {
            var absolute = !string.IsNullOrEmpty(BaseAddress);
            WebHelper.Redirect(BuildQuery(absolute), Context);
        }

        public string GetSource()
        {
            return GetDecode(SourceKey);
        }

        public void RedirectToSource()
        {
            string source = GetDecode(QueryParser.SourceKey);
            Remove(SourceKey);
            BaseRedirect(BuildQuery(source));
        }

        public void Redirect(string basePath)
        {
            WebHelper.Redirect(BuildQuery(basePath), Context);
        }

        public void SetSourceAndRedirect(string basePath)
        {
            //Set(SourceKey, EncodedBasePath);
            SetEncode(SourceKey, EncodedBasePath);
            Redirect(basePath);
        }

        public int GetInt32(string name)
        {
            return DataHelper.GetInt32(this[name]);
        }

        public int GetInt32(string name, int defaultValue)
        {
            return DataHelper.GetInt32(this[name], defaultValue);
        }

        public int GetId(string name)
        {
            return DataHelper.GetId(this[name]);
        }
        public bool GetBool(string name, bool defaultValue)
        {
            return DataHelper.GetBool(this[name], defaultValue);
        }

        //public string SetAndBuildQuery(string name, object value, string basePath)
        //{
        //    Set(name, value);

        //    return BuildQuery(basePath);
        //}

        public QueryParser Set(string name, object value)
        {
            base[name] = value == null ? "" : value.ToString();
            return this;
        }

        //public QueryParser SetAndReturn(string name, object value)
        //{
        //    base[name] = value == null ? "" : value.ToString();

        //    return this;
        //}

        public void SetReturn(string returnUrl = "")
        {
            if (string.IsNullOrEmpty(returnUrl))
                SetEncode(SourceKey, BuildQuery());
            else
                SetEncode(SourceKey, returnUrl);
        }

        public bool TryReturn()
        {
            var returnUrl = GetDecode(SourceKey);
            if (!string.IsNullOrEmpty(returnUrl))
            {
                StaticRedirect(returnUrl);
                return true;
            }

            return false;
        }

        public void SetSource(string value)
        {
            // (Temporary)  Observe if working properly to prevent ugly urls (22-May)
            //SetEncode(SourceKey, value.Replace("&", "|"));

            SetEncode(SourceKey, value);
        }

        public string GetValue(string name, string nullEmptyDefaultValue)
        {
            string value = Get(name);
            return string.IsNullOrEmpty(value) ? nullEmptyDefaultValue : value;
        }

        public virtual QueryParser Clone()
        {
            return new QueryParser(this);
        }

        public void SetEncode(string key, string value)
        {
            //this[key] = HttpUtility.UrlEncode(value);

            // (Temporary)  Observe if working properly to prevent ugly urls (22-May)
            //SetEncode(SourceKey, value.Replace("&", "|"));

            Set(key, value.Replace("&", "|"));
        }

        public string GetDecode(string key)
        {
            string value = this[key];
            if (!string.IsNullOrEmpty(value))
                return HttpUtility.UrlDecode(value).Replace("|", "&");

            return string.Empty;
        }

        #endregion

        #region Static Methods

        public static string BuildQuery(Control instance)
        {
            var query = new QueryParser(instance);
            return query.BuildQuery();
        }

        public static string BuildQuery(string url, string name, object value)
        {
            var query = new QueryParser(url);
            query.Set(name, value);

            return query.BuildQuery();
        }

        public static void StaticBaseRedirect(string basePath)
        {
            WebHelper.Redirect(basePath, Context);
        }

        public static void StaticRedirect()
        {
            var query = new QueryParser(HttpContext.Current);
            query.Redirect();
        }

        public static void StaticRedirect(string addressOrBasePath, bool retainParameters)
        {
            if (retainParameters)
            {
                var query = new QueryParser(Context);
                query.Redirect(addressOrBasePath);
            }
            else
            {
                WebHelper.Redirect(addressOrBasePath, Context);
            }
        }

        public static void StaticRedirect(string basePath)
        {
            StaticRedirect(basePath, false);
        }

        public static string StaticGet(string key, HttpRequest request)
        {
            return request[key];
        }

        #endregion
    }
}
