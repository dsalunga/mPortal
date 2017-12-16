<%@ WebService Language="C#" Class="FlashService" %>

using System;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using System.Xml;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FlashService  : System.Web.Services.WebService {

    [WebMethod]
    public string GetBannerXML()
    {
        StringBuilder sb = new StringBuilder();
        TextWriter tw = new StringWriter(sb);
        XmlTextWriter xw = new XmlTextWriter(tw);
        
        xw.Formatting = Formatting.None;
        xw.WriteStartDocument();
        xw.WriteStartElement("FlashBanner");
        
        // STAGE
        xw.WriteAttributeString("align", "TL");
        xw.WriteAttributeString("showMenu", "false");
        xw.WriteAttributeString("alpha", "90");

        // LOADER
        xw.WriteStartElement("Loader");
        xw.WriteAttributeString("width", "800");
        xw.WriteAttributeString("height", "600");
        xw.WriteAttributeString("interval", "1500");
        xw.WriteAttributeString("repeat", "true");

        string[] sImages = new string[] { 
            "/Content/Parts/FlashBanner/Images/010720062225.jpg", 
            "/Content/Parts/FlashBanner/Images/ourhome.GIF", 
            "/Content/Parts/FlashBanner/Images/Longhorn.jpg",
            "/Content/Parts/FlashBanner/Images/project.jpg"
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

        return sb.ToString();
    }
}