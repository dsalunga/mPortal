namespace WCMS.WebSystem.Content.Parts.Integration.GlobalSwitch
{
    public class GlobalSwitchHelper
    {
        public static string Render(string template, dynamic context = null)
        {
            /*
            var context = HttpContext.Current;

            var model = new {
                    WSession = WCMS.Framework.WSession.Current,
                    RawUrl = context.Request.RawUrl
                };
            var content = RazorHelper.RenderPage(template,
                model, "Integration.GlobalSwitch");
            */

            return RazorHelper.RenderPage(template, new { Context = context }, "Integration.GlobalSwitch");
        }
    }
}