using WCMS.WebSystem;

namespace WCMS.WebSystem.Content.Parts.Integration.GlobalSwitch
{
    public class GlobalSwitchHelper
    {
        public static string Render(string template, dynamic context = null)
        {
            return RazorHelper.RenderPage(template, new { Context = context }, "Integration.GlobalSwitch");
        }
    }
}