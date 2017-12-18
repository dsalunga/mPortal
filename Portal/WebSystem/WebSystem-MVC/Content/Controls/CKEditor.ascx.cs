using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.Controls
{
    public partial class CKEditor : System.Web.UI.UserControl
    {
        private const string TabPlainText = "tabText";
        private const string TabFCKText = "tabHtml";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TabControl1.Tabs.Count == 0)
            {
                TabControl1.AddTab(TabPlainText, "Plain Text");
                TabControl1.AddTab(TabFCKText, "HTML Editor");
                TabControl1.SelectedIndex = _isPlainTextDefault ? 0 : 1;

                if (!_isPlainTextDefault)
                    SetActiveEditor(TabFCKText);
            }
        }

        public void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            SetActiveEditor(args.TabName);
        }

        private void SetActiveEditor(string tabName)
        {
            switch (tabName)
            {
                case TabPlainText:
                    {
                        SetEditorValue(TabPlainText);

                        MultiView1.SetActiveView(viewText);
                        break;
                    }

                case TabFCKText:
                    {
                        SetEditorValue(TabFCKText);

                        MultiView1.SetActiveView(viewHtml);
                        break;
                    }
            }
        }

        private void SetEditorValue(string newTab)
        {
            string value = string.Empty;

            if (MultiView1.ActiveViewIndex == 0)
                value = txtValueText.Text;
            else if (MultiView1.ActiveViewIndex == 1)
                value = txtValue.Text;

            switch (newTab)
            {
                case TabPlainText:
                    txtValueText.Text = value;
                    break;

                case TabFCKText:
                    txtValue.Text = value;
                    break;
            }
        }

        private string GetEditorValue()
        {
            var tab = TabControl1.SelectedIndex;

            switch (tab)
            {
                case 0:
                    return txtValueText.Text;

                case 1:
                    return txtValue.Text;
            }

            return string.Empty;
        }

        public string Text
        {
            get
            {
                return GetEditorValue();
            }

            set
            {
                var tab = TabControl1.SelectedIndex;

                switch (tab)
                {
                    case 0:
                        txtValueText.Text = value;
                        break;

                    case 1:
                        txtValue.Text = value;
                        break;
                }
            }
        }

        private bool _isPlainTextDefault = true;
        public bool IsPlainTextDefault
        {
            get { return _isPlainTextDefault; }
            set
            {
                _isPlainTextDefault = value;
            }
        }

        private string _height;
        public string Height
        {
            get { return _height; }
            set
            {
                _height = value;
                txtValue.Height = new Unit(value);
                txtValueText.Height = new Unit(value);
            }
        }
    }
}