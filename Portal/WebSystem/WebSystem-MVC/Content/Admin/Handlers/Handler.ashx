<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using des.Utils;


public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        string sSection = context.Request.QueryString["Section"];
        string sID = context.Request.QueryString["ID"];
        
        // Set up the response settings
        context.Response.ContentType = "image/jpeg";
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.BufferOutput = false;

        Stream stream = null;
        object obj = null;

        switch (context.Request.QueryString["Section"])
        {
            case "SectionTemplate":
                obj = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT Thumbnail FROM CMS.CommonSectionItemTemplates WHERE CommonSectionItemTemplateID=@CommonSectionItemTemplateID",
                    new SqlParameter("@CommonSectionItemTemplateID", Convert.ToInt32(sID))
                    );

                if (obj != null)
                {
                    stream = new MemoryStream((byte[])obj);
                }
                break;
        }

        if (stream != null)
        {
            // Write image stream to the response stream
            const int buffersize = 1024 * 16;
            byte[] buffer = new byte[buffersize];
            int count = stream.Read(buffer, 0, buffersize);
            while (count > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, count);
                count = stream.Read(buffer, 0, buffersize);
            }
        }
        else
        {
            context.Response.WriteFile("/_Sections/Thumb.jpg");
            
            /*
            using (FileStream fs = new FileStream(context.Request.MapPath("/_Sections/Thumb.jpg"), FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    //context.Response.OutputStream.Write(br.ReadBytes((int)fs.Length), 0, (int)fs.Length);

                    const int buffersize = 1024 * 16;
                    byte[] buffer = new byte[buffersize];
                    int count = br.Read(buffer, 0, buffersize);
                    while (count > 0)
                    {
                        context.Response.OutputStream.Write(buffer, 0, count);
                        count = br.Read(buffer, 0, buffersize);
                    }
                }
            }
            */
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}