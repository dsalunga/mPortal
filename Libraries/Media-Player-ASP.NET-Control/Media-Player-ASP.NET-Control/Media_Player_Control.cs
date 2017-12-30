#region namespaces
using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

namespace Media_Player_ASP.NET_Control
{
    [DefaultProperty("MovieURL"), ToolboxData("<{0}:Media_Player_Control runat=server></{0}:Media_Player_Control>")]
    public class Media_Player_Control : System.Web.UI.WebControls.WebControl
    {

        #region Default Property Values
        const string DefaultMovieURL = "";
        const bool DefaultAutoStart = false;
        const int DefaultBalance = 0;
        const int DefaultCurrentPosition = 0;
        const bool DefaultEnableContextMenu = true;
        const bool DefaultFullScreen = false;
        const bool DefaultMute = false;
        const int DefaultPlayCount = 1;
        const System.Single DefaultRate = (float)1.0;
        const bool DefaultStretchToFit = false;
        const Enumerations.PlayerMode DefaultUIMode = Enumerations.PlayerMode.Full;
        const int DefaultVolume = -1;
        #endregion

        #region Constructor
        public Media_Player_Control()
        {
            //Default width and height of the player
            this.Width = System.Web.UI.WebControls.Unit.Pixel(320);
            this.Height = System.Web.UI.WebControls.Unit.Pixel(240);
        }
        #endregion

        #region Public properties

        [Bindable(true), Category("Settings"), Description("Absolute or relative URL to movie.")]
        public string MovieURL
        {
            get
            {
                if (ViewState["MovieURL"] == null)
                    return DefaultMovieURL;
                else
                    return (string)ViewState["MovieURL"];
            }
            set
            {
                if (value != DefaultMovieURL)
                    ViewState["MovieURL"] = value;
                else
                    ViewState["MovieURL"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("Would movie play when page is loaded.")]
        public bool AutoStart
        {
            get
            {
                if (ViewState["AutoStart"] == null)
                    return DefaultAutoStart;
                else
                    return (bool)ViewState["AutoStart"];
            }
            set
            {
                if (value != DefaultAutoStart)
                    ViewState["AutoStart"] = value;
                else
                    ViewState["AutoStart"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("Balance")]
        int Balance
        {
            get
            {
                if (ViewState["Balance"] == null)
                    return DefaultBalance;
                else
                    return (int)ViewState["Balance"];
            }
            set
            {
                if (value > 100)
                    value = 100;
                if (value < -100)
                    value = -100;
                if (value != DefaultBalance)
                    ViewState["Balance"] = value;
                else
                    ViewState["Balance"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("Current postion.")]
        public int CurrentPosition
        {
            get
            {
                if (ViewState["CurrentPosition"] == null)
                    return DefaultCurrentPosition;
                else
                    return (int)ViewState["CurrentPosition"];
            }
            set
            {
                if (value < 0)
                    value = 0;
                if (value != DefaultCurrentPosition)
                    ViewState["CurrentPosition"] = value;
                else
                    ViewState["CurrentPosition"] = null;
            }
        }

        [Bindable(true), Category("Settings")]
        public bool EnableContextMenu
        {
            get
            {
                if (ViewState["EnableContextMenu"] == null)
                    return DefaultEnableContextMenu;
                else
                    return (bool)ViewState["EnableContextMenu"];
            }
            set
            {
                if (value != DefaultEnableContextMenu)
                    ViewState["EnablecontextMenu"] = value;
                else
                    ViewState["EnablecontextMenu"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("Would movie be played in full screen.")]
        public bool FullScreen
        {
            get
            {
                if (ViewState["FullScreen"] == null)
                    return DefaultFullScreen;
                else
                    return (bool)ViewState["FullScreen"];
            }
            set
            {
                if (value != DefaultFullScreen)
                    ViewState["FullScreen"] = value;
                else
                    ViewState["FullScreen"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("Play video without sound.")]
        public bool Mute
        {
            get
            {
                if (ViewState["Mute"] == null)
                    return DefaultMute;
                else
                    return (bool)ViewState["Mute"];
            }
            set
            {
                if (value != DefaultMute)
                    ViewState["Mute"] = value;
                else
                    ViewState["Mute"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("How much times will video play.")]
        int PlayCount
        {
            get
            {
                if (ViewState["PlayCount"] == null)
                    return DefaultPlayCount;
                else
                    return (int)ViewState["PlayCount"];
            }
            set
            {
                if (value < 1)
                    value = 1;
                if (value != DefaultPlayCount)
                    ViewState["PlayCount"] = value;
                else
                    ViewState["PlayCount"] = null;
            }
        }

        [Bindable(true), Category("Settings")]
        System.Single Rate
        {
            get
            {
                if (ViewState["Rate"] == null)
                    return DefaultRate;
                else
                    return (float)ViewState["Rate"];
            }
            set
            {
                if (value < 0.0)
                    value = (float)1.0;
                if (value != DefaultRate)
                    ViewState["Rate"] = value;
                else
                    ViewState["Rate"] = null;
            }
        }

        [Bindable(true), Category("Settings")]
        public bool StretchToFit
        {
            get
            {
                if (ViewState["StretchToFit"] == null)
                    return DefaultStretchToFit;
                else
                    return (bool)ViewState["StretchToFit"];
            }
            set
            {
                if (value != DefaultStretchToFit)
                    ViewState["StretchToFit"] = value;
                else
                    ViewState["StretchToFit"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("Set how player will look like.")]
        public Enumerations.PlayerMode uiMode
        {
            get
            {
                if (ViewState["uiMode"] == null)
                    return DefaultUIMode;
                else
                    return (Enumerations.PlayerMode)ViewState["uiMode"];
            }
            set
            {
                if (value != DefaultUIMode)
                    ViewState["uiMode"] = value;
                else
                    ViewState["uiMode"] = null;
            }
        }

        [Bindable(true), Category("Settings"), Description("Set sound volume")]
        public int Volume
        {
            get
            {
                if (ViewState["Volume"] == null)
                    return DefaultVolume;
                else
                    return (int)ViewState["Volume"];
            }
            set
            {
                if (value != DefaultVolume)
                    ViewState["Volume"] = value;
                else
                    ViewState["Volume"] = null;
            }
        }
        #endregion

        #region Private functions
        private string getPlayerMode()
        {
            switch (uiMode)
            {
                case Enumerations.PlayerMode.Invisible:
                    return "invisible";
                case Enumerations.PlayerMode.Mini:
                    return "mini";
                case Enumerations.PlayerMode.None:
                    return "none";
                default:
                    return "full";
            }
        }
        #endregion

        #region Render
        [Description("Render control, depending of property values")]
        protected override void RenderContents(HtmlTextWriter output)
        {
            StringBuilder content = new StringBuilder();
            content.Append("<OBJECT width=\"" + Width.ToString() + "\" height=\"" + Height.ToString() +
                  "\" CLASSID=\"CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6\" VIEWASTEXT>\n");
            content.Append("<PARAM name=\"autoStart\" value=\"" + AutoStart + "\">\n");
            content.Append("<PARAM name=\"URL\" value=\"" + MovieURL + "\">\n");
            content.Append("<PARAM name=\"enabled\" value=\"" + Enabled + "\">\n");
            content.Append("<PARAM name=\"balance\" value=\"" + Balance + "\">\n");
            content.Append("<PARAM name=\"currentPosition\" value=\"" + CurrentPosition + "\">\n");
            content.Append("<PARAM name=\"enableContextMenu\" value=\"" + EnableContextMenu + "\">\n");
            content.Append("<PARAM name=\"fullScreen\" value=\"" + FullScreen + "\">\n");
            content.Append("<PARAM name=\"mute\" value=\"" + Mute + "\">\n");
            content.Append("<PARAM name=\"playCount\" value=\"" + PlayCount + "\"\n>");
            content.Append("<PARAM name=\"rate\" value=\"" + Rate + "\"\n>");
            content.Append("<PARAM name=\"stretchToFit\" value=\"" + StretchToFit + "\"\n>");
            content.Append("<PARAM name=\"uiMode\" value=\"" + getPlayerMode() + "\"\n>");
            if (Volume >= 0)
                content.Append("<PARAM name=\"volume\" value=\"" + Volume + "\"\n>");
            content.Append("</OBJECT>");
            output.Write(content.ToString());
        }
    }
        #endregion

    #region Enumerations
    namespace Enumerations
    {
        [Description("Player mode can be Invisible, None, Mini and Full")]
        public enum PlayerMode
        {
            Invisible = 0,
            None = 1,
            Mini = 2,
            Full = 3
        }
    }
    #endregion
}