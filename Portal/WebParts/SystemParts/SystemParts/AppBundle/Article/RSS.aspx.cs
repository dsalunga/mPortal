using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using WCMS.Framework;
using WCMS.Common.Utilities;
using WCMS.WebSystem.WebParts.Article;

namespace WCMS.WebSystem.WebParts.Article
{
    public partial class _Sections_P_RSS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string strPath = Request.Url.IsDefaultPort ? "http://" + Request.Url.Host : "http://" + Request.Url.Host + ":" + Request.Url.Port.ToString();

            int query = Request.QueryString.Count > 0 ? DataHelper.GetId(Request.QueryString[0]) : -1;
            if (!(query > 0))
            {
                Response.Redirect("/", false);
            }
            else
            {
                //try
                //{
                //    int intID = int.Parse(query);
                //}
                //catch
                //{
                //    Response.Redirect(strPath, true);
                //}
            }

            var page = WPage.Get(query);
            var qs = new QueryParser(page.BuildRelativeUrl());

            Response.ContentType = "text/xml";
            var xws = new XmlWriterSettings();
            xws.Indent = true;
            xws.OmitXmlDeclaration = true;
            using (XmlWriter xw = XmlWriter.Create(Response.Output, xws))
            {
                xw.WriteStartElement("rss");
                xw.WriteAttributeString("version", "2.0");
                xw.WriteStartElement("channel");
                xw.WriteElementString("title", "RSS Feeds");
                xw.WriteElementString("link", page.BuildRelativeUrl());
                xw.WriteElementString("description", "RSS Feeds");

                var articles = ArticleLink.GetList(WebObjects.WebPage, query);

                foreach (var articleLocation in articles)
                {
                    var article = articleLocation.Article;

                    qs[Article.ArticleKey] = article.Id.ToString();

                    xw.WriteStartElement("item");
                    xw.WriteElementString("title", article.Title);
                    xw.WriteElementString("description", article.Description);
                    xw.WriteElementString("link", qs.BuildQuery());
                    xw.WriteElementString("author", article.Author);
                    xw.WriteElementString("pubDate", article.Date.ToString("R"));
                    xw.WriteEndElement();
                }

                //using (SqlDataReader dr = SqlHelper.ExecuteReader("P.LocationSelect",
                //    new SqlParameter("@PageType", "S"),
                //    new SqlParameter("@ItemID", query),
                //    new SqlParameter("@IsOut", false)))
                //{
                //    while (dr.Read())
                //    {
                //        xw.WriteStartElement("item");
                //        xw.WriteElementString("title", dr["Title"].ToString());
                //        xw.WriteElementString("description", dr["description"].ToString());
                //        xw.WriteElementString("link", strPath + "/?SS=" + query + "&ShowID=" + dr["ID"].ToString());
                //        xw.WriteElementString("author", dr["Author"].ToString());
                //        xw.WriteElementString("pubDate", ((DateTime)dr["Date"]).ToString("R"));
                //        xw.WriteEndElement();
                //    }
                //}

                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }
    }
}