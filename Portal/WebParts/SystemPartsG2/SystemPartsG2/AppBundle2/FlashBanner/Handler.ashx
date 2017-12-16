<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.IO;
using System.Web;
using System.Text;
using System.Xml;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/xml";
        //context.Response.Write("Hello World");
        //context.Response.Cache.SetCacheability(HttpCacheability.Public);
        //context.Response.BufferOutput = false;

        StringBuilder sb = new StringBuilder();
        TextWriter tw = new StringWriter(sb);
        XmlTextWriter xw = new XmlTextWriter(tw);

        xw.Formatting = Formatting.None;
        //xw.WriteStartDocument();
        xw.WriteStartElement("FlashBanner");

        // STAGE
        xw.WriteAttributeString("align", "TL");
        xw.WriteAttributeString("showMenu", "false");
        xw.WriteAttributeString("alpha", "30");

        // LOADER
        xw.WriteStartElement("Loader");
        xw.WriteAttributeString("width", "800");
        xw.WriteAttributeString("height", "600");
        xw.WriteAttributeString("interval", "1500");
        xw.WriteAttributeString("repeat", "true");

        string[] sImages = new string[] { 
            "Images/010720062225.jpg", 
            "Images/ourhome.GIF", 
            "Images/Longhorn.jpg",
            "Images/project.jpg"
        };
        for (int i = 0; i < sImages.Length; i++)
        {
            xw.WriteStartElement("Image");
            xw.WriteAttributeString("id", i.ToString());
            xw.WriteStartElement("Source");
            xw.WriteValue(sImages[i]);
            xw.WriteEndElement();
            xw.WriteEndElement();
        }

        xw.Flush();
        xw.Close();

        context.Response.Write(sb.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}