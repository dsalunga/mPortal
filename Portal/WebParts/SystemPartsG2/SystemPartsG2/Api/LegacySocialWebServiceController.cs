using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCMS.Framework.Core;
using WCMS.Framework.Social;

namespace WCMS.WebSystem.Api
{
    /// <summary>
    /// Legacy-compatible replacement for Social/WebService.asmx.
    /// Returns ASMX script-service payload shape: { d: ... }.
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class LegacySocialWebServiceController : ControllerBase
    {
        [HttpGet("/Content/Parts/Social/WebService.asmx/HelloWorld")]
        [HttpPost("/Content/Parts/Social/WebService.asmx/HelloWorld")]
        [HttpGet("/Content/Parts/AppBundle2/Social/WebService.asmx/HelloWorld")]
        [HttpPost("/Content/Parts/AppBundle2/Social/WebService.asmx/HelloWorld")]
        public IActionResult HelloWorld()
        {
            return new JsonResult(new { d = "Hello World" });
        }

        [HttpPost("/Content/Parts/Social/WebService.asmx/DeleteWallEntry")]
        [HttpPost("/Content/Parts/AppBundle2/Social/WebService.asmx/DeleteWallEntry")]
        [HttpPost("/_Sections/Social/WebService.asmx/DeleteWallEntry")]
        public IActionResult DeleteWallEntry([FromBody] DeleteWallEntryRequest request)
        {
            var result = DeleteWallEntryCore(request?.Id ?? 0);
            return new JsonResult(new { d = result });
        }

        private static bool DeleteWallEntryCore(int id)
        {
            if (id <= 0)
                return false;

            var wallEntry = WallUpdate.Provider.Get(id);
            if (wallEntry == null)
                return false;

            try
            {
                var comments = WebComment.Provider.GetList(-2, wallEntry.OBJECT_ID, wallEntry.Id, -2);
                if (comments != null && comments.Any())
                {
                    foreach (var comment in comments)
                        comment.Delete();
                }
            }
            catch
            {
                // Keep legacy behavior: ignore comment deletion failures and continue.
            }

            return wallEntry.Delete();
        }
    }

    public class DeleteWallEntryRequest
    {
        public int Id { get; set; }
    }
}
