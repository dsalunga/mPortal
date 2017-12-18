using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.Controls
{
    public partial class TextEditor : UserControl, ITextEditor
    {
        private const string TabPlainText = "tabText";
        //private const string TabAjaxText = "tabAjaxEditor";
        private const string TabFCKText = "tabHtml";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TabControl1.Tabs.Count == 0)
            {
                TabControl1.AddTab(TabPlainText, "Plain Text");
                //TabControl1.AddTab(TabAjaxText, "Ajax Editor");
                TabControl1.AddTab(TabFCKText, "HTML Editor");
                TabControl1.SelectedIndex = _isPlainTextDefault ? 0 : 1;
            }

            if (!_isPlainTextDefault)
                SetActiveEditor(TabFCKText);
            //else
            //    SetActiveEditor(TabPlainText);
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }

        public void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            SetActiveEditor(args.TabName);
        }

        public void SetPlainTextEditor()
        {
            TabControl1.SelectedIndex = 0;
            SetActiveEditor(TabPlainText);
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

                //case TabAjaxText:
                //    {
                //        SetEditorValue(TabAjaxText);

                //        MultiView1.SetActiveView(viewAjaxEditor);
                //        break;
                //    }

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

            if (MultiView1.ActiveViewIndex == 0) //(!string.IsNullOrEmpty(txtValueText.Text))
                value = txtValueText.Text;
            else if (MultiView1.ActiveViewIndex == 1) //(!string.IsNullOrEmpty(txtValue.Value))
                value = txtValue.Value;

            switch (newTab)
            {
                case TabPlainText:
                    txtValueText.Text = value;
                    break;

                //case TabAjaxText:
                //    txtAjaxValue.Content = value;
                //    break;

                case TabFCKText:
                    txtValue.Value = value;
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

                //case 1:
                //    return txtAjaxValue.Content;

                case 1:
                    return txtValue.Value;
            }

            return string.Empty;
        }

        public string Text
        {
            get { return GetEditorValue(); }

            set
            {
                var tab = TabControl1.SelectedIndex;

                //if (TabControl1.SelectedIndex == 0)
                //    txtValueText.Text = value;
                //else
                //    txtValue.Value = value;

                switch (tab)
                {
                    case 0:
                        txtValueText.Text = value;
                        break;

                    //case 1:
                    //    txtAjaxValue.Content = value;
                    //    break;

                    case 1:
                        txtValue.Value = value;
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
                //TabControl1.SelectedIndex = value ? 0 : 1;
            }
        }

        private string _editorToolbarSet;
        public string EditorToolbarSet
        {
            get { return _editorToolbarSet; }
            set
            {
                _editorToolbarSet = value;
                txtValue.ToolbarSet = value;
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