using System;
using System.Configuration;



namespace WCMS
{
    /// <summary>
    /// Summary description for TextHelper.
    /// </summary>
    public abstract class TextHelper
    {
        // CONSTANTS
        private const string C_CSS_EDITOR = "<link href=\"/Content/Assets/Styles/TemplateEditor.css\" type=\"text/css\" rel=\"stylesheet\" /><link href=\"../../_CSS/Portal.css\" type=\"text/css\" rel=\"stylesheet\" />";
        private const string C_CODE_DIVIDER = "<!-- CODE_DIVIDER -->";

        private const string C_PH = "<asp:PlaceHolder ID=\"[ID]\" Runat=\"server\"></asp:PlaceHolder>";
        private const string C_PH_ = "<div class=\"PlaceHolder\" align=\"center\">[ID]</div>";

        private const string C_SECTION_TITLE = "<asp:Literal ID=\"lSectionTitle\" Runat=\"server\"></asp:Literal>";
        private const string C_SECTION_TITLE_ = "<div class=\"SectionTitle\" align=\"center\">[ SECTION TITLE ]</div>";

        private const string C_PH_OPEN = "<asp:PlaceHolder";
        private const string C_PH_CLOSE = "</asp:PlaceHolder>";
        private const string C_PH_ID = " ID=\"";

        private const string C_DIRECTIVE_OPEN = "<%@";
        private const string C_DIRECTIVE_OPEN_ = "<!--%@";

        private const string C_CODE_OPEN = "<%";
        private const string C_CODE_CLOSE = "%>";
        private const string C_DIRECTIVE_CLOSE_ = "@%-->";
        private const string C_CONTROL_OPEN = "<asp:";
        private const string C_CONTROL_CLOSE = "</asp:";
        private const string C_BINDER_OPEN = "<%#";

        public static string EncodeControlTemplate(string src)
        {
            string dest = EncodePageDirectives(src);
            dest = EncodePlaceHolders(dest);
            dest = EncodeFilesPath(dest);
            dest = EncodesectionTitle(dest);

            return C_CSS_EDITOR + C_CODE_DIVIDER + dest;

            // ALL DATA BINDER <%# %>
            // ALL OUTPUT VARIABLES <%= %>
            // ALL SERVER COdes <% %>
            // ALL SERVER CONTROLS <asp:_ /asp:_
        }

        private static string EncodeFilesPath(string src)
        {
            string dest = string.Empty;

            dest = src.Replace("\"Assets/Uploads/Image/", "\"" + ConfigurationManager.AppSettings["WEB_ROOT"] + "/Assets/Uploads/Image/");

            return dest;
        }

        private static string EncodePageDirectives(string src)
        {
            string dest = string.Empty;
            int cPos = 0;
            int cPos1 = 0;
            int cPos2 = 0;

            // PAGE DIRECTIVE FIRST
            while (true)
            {
                // FIND THE <%@
                cPos1 = src.IndexOf(C_DIRECTIVE_OPEN, cPos);
                if (cPos1 != -1)
                {
                    // GET PRE-DIRECTIVE STRING
                    dest += src.Substring(cPos, cPos1 - cPos);
                    cPos += cPos1 - cPos;

                    // APPEND THE BEGINING ENCODED STRING
                    dest += C_DIRECTIVE_OPEN_;
                    // MOVE THE CURRENT POSITION AFTER THE OPENING TAG
                    cPos += C_DIRECTIVE_OPEN.Length;
                }
                else
                    break;

                // FIND THE %>
                cPos2 = src.IndexOf(C_CODE_CLOSE, cPos);
                if (cPos2 != -1)
                {
                    // APPEND CODE IN BETWEEN THE OPENING ANG CLOSING TAG
                    dest += src.Substring(cPos, cPos2 - cPos);
                    // MOVE THE CURRENT POSITION BEFORE THE CLOSING TAG
                    cPos += cPos2 - cPos;

                    // APPEND THE CLOSING ENCODED STRING
                    dest += C_DIRECTIVE_CLOSE_;
                    // MOVE THE CURRENT POSITION AFTER THE CLOSING TAG
                    cPos += C_CODE_CLOSE.Length;
                }
                else
                    break;
            }
            dest += src.Substring(cPos);

            return dest;
        }

        private static string EncodePlaceHolders(string src)
        {
            // ###########################################################
            string dest = string.Empty;
            int cPos = 0;
            int cPos1 = 0;
            int cPos2 = 0;

            // ALL PLACEHOLDERS
            while (true)
            {
                cPos1 = src.IndexOf(C_PH_OPEN, cPos);
                cPos2 = src.IndexOf(C_PH_CLOSE, cPos);

                if ((cPos1 != -1 && cPos2 != -1) || (cPos1 > cPos2))
                {
                    int idPos = src.IndexOf(C_PH_ID, cPos1) + C_PH_ID.Length;
                    int idPosEnd = src.IndexOf("\"", idPos);
                    string sID = src.Substring(idPos, idPosEnd - idPos);

                    // GET PRE-DIRECTIVE STRING
                    dest += src.Substring(cPos, cPos1 - cPos);
                    cPos += cPos1 - cPos;

                    dest += C_PH_.Replace("[ID]", sID);
                    cPos += (cPos2 + C_PH_CLOSE.Length) - cPos1;
                }
                else
                    break;
            }
            dest += src.Substring(cPos);

            return dest;
        }

        private static string EncodesectionTitle(string src)
        {
            return src.Replace(C_SECTION_TITLE, C_SECTION_TITLE_);
        }

        public static string SetModeA(string src)
        {
            return src.Replace("class=\"PlaceHolderModeB\"", "class=\"PlaceHolder\"");
        }

        public static string SetModeB(string src)
        {
            return src.Replace("class=\"PlaceHolder\"", "class=\"PlaceHolderModeB\"");
        }

        public static string EncodeAbsolutePath(string src)
        {
            string dest = string.Empty;

            string sPath = ConfigurationManager.AppSettings["WEB_PATH"];
            string sRoot = ConfigurationManager.AppSettings["WEB_ROOT"];

            if (sRoot == "/")
            {
                sPath = sPath.TrimEnd('/') + "/";
            }
            else
            {
                sRoot = sRoot.TrimEnd('/') + "/";
                sPath = sPath.TrimEnd('/') + "/";
            }

            dest = src.Replace("=\"" + sRoot, "=\"" + sPath);
            //dest = dest.Replace("=\"Uploads/", "=\"" + sPath + "Uploads/");
            dest = dest.Replace("=\"_CSS/", "=\"" + sPath + "_CSS/");
            dest = dest.Replace("=\"_js/", "=\"" + sPath + "_js/");
            dest = dest.Replace("=\"?", "=\"" + sPath + "?");
            dest = dest.Replace("=\".?", "=\"" + sPath + "?");

            return dest;
        }

        public static string DecodeControlTemplate(string src)
        {
            string dest = DecodePageDirectives(src);
            dest = DecodePlaceHolders(dest);
            dest = DecodeFilesPath(dest);
            dest = DecodesectionTitle(dest);

            return C_CSS_EDITOR + C_CODE_DIVIDER;

            // ALL DATA BINDER <%# %>
            // ALL OUTPUT VARIABLES <%= %>
            // ALL SERVER COdes <% %>
            // ALL SERVER CONTROLS <asp:_ /asp:_
        }

        private static string DecodePageDirectives(string src)
        {
            string dest = string.Empty;

            dest = src.Replace(C_DIRECTIVE_OPEN_, C_DIRECTIVE_OPEN);
            dest = dest.Replace(C_DIRECTIVE_CLOSE_, C_CODE_CLOSE);

            return dest;
        }

        private static string DecodePlaceHolders(string src)
        {
            // START DECODE 

            return src;
        }

        private static string DecodeFilesPath(string src)
        {
            return src;
        }

        private static string DecodesectionTitle(string src)
        {
            return src;
        }
    }
}
