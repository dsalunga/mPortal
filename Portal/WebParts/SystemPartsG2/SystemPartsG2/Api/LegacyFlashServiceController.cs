using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for AppBundle2/FlashBanner/FlashService.asmx.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacyFlashServiceController : ControllerBase
    {
        private static readonly string[] BannerImages =
        {
            "/Content/Parts/FlashBanner/Images/010720062225.jpg",
            "/Content/Parts/FlashBanner/Images/ourhome.GIF",
            "/Content/Parts/FlashBanner/Images/Longhorn.jpg",
            "/Content/Parts/FlashBanner/Images/project.jpg"
        };

        [HttpGet("/Content/Parts/AppBundle2/FlashBanner/FlashService.asmx")]
        [HttpGet("/Content/Parts/AppBundle2/FlashBanner/FlashService.asmx/GetBannerXML")]
        [HttpGet("/_Sections/FlashBanner/FlashService.asmx")]
        [HttpGet("/_Sections/FlashBanner/FlashService.asmx/GetBannerXML")]
        public IActionResult GetBannerXml()
        {
            Response.ContentType = "text/xml";
            return Content(BuildBannerXml(), "text/xml", Encoding.UTF8);
        }

        private static string BuildBannerXml()
        {
            var root = new XElement("FlashBanner",
                new XAttribute("align", "TL"),
                new XAttribute("showMenu", "false"),
                new XAttribute("alpha", "90"),
                new XElement("Loader",
                    new XAttribute("width", "800"),
                    new XAttribute("height", "600"),
                    new XAttribute("interval", "1500"),
                    new XAttribute("repeat", "true"),
                    BannerImages.Select((image, index) =>
                        new XElement("Image",
                            new XAttribute("id", index.ToString(CultureInfo.InvariantCulture)),
                            new XElement("Source", image)))));

            return root.ToString(SaveOptions.DisableFormatting);
        }
    }
}
