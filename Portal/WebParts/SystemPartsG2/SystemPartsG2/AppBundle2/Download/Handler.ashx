<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Download
{
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sDownloadID = context.Request.QueryString["ID"];

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.BufferOutput = false;

            object obj = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT Filename FROM Download.Downloads WHERE DownloadID=@DownloadID",
                new SqlParameter("@DownloadID", Convert.ToInt32(sDownloadID))
            );

            if (obj != null)
            {
                string sFilename = obj.ToString();

                if (context.Request.QueryString["Force"] == "true")
                {
                    // Force Download
                    context.Response.AppendHeader("content-disposition", "attachment; filename=" + sFilename);
                }

                context.Response.WriteFile("/Assets/Uploads/Image/SECTIONS/Download/" + obj);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}