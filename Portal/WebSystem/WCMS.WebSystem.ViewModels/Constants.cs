using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem
{
    public static class WSConstants
    {
        public const string ElementDesignerPath = "~/Content/Parts/Central/Controls/ElementDesigner.ascx";
        public const string RazorDesignModeControlPath = "~/Content/Parts/Central/Controls/DesignMode.cshtml";
        public const string PART_PATH_FORMAT = "~/Content/Themes/{0}/Parts/{1}/{2}/{3}";
        public const string PanelDesignerPath = "~/Content/Parts/Central/Controls/PanelDesigner.ascx";
        public const string DesignModeControlPath = "~/Content/Parts/Central/Controls/DesignMode.ascx";
        public const string LoadingErrorMsg = "Failed loading control: {0}";
        public const string SetupPath = "~/Central/Setup";

        public const string NO_LOAD = "No configuration available for this element.";
        public const string ERROR_LOAD = "Error loading module: ";

    }
}
