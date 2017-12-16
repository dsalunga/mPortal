using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

using WCMS.WebSystem.Controls;

using WCMS.Common.Utilities;

using WCMS.Framework;

using WCMS.WebSystem.WebParts.Menu.Managers;

namespace WCMS.WebSystem.WebParts.Menu
{
    public partial class ImportExport : System.Web.UI.UserControl
    {
        private const string TAB_EXPORT = "tabExport";
        private const string TAB_IMPORT = "tabImport";

        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TabControl1.AddTab(TAB_EXPORT, "Export");
                TabControl1.AddTab(TAB_IMPORT, "Import");
            }
        }

        public DataSet GetList()
        {
            return DataHelper.ToDataSet(
                from item in MenuEntity.Provider.GetList()
                select new
                {
                    item.Id,
                    item.Name
                }
            );
        }

        protected void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case TAB_EXPORT:
                    MultiView1.SetActiveView(viewExport);
                    break;

                case TAB_IMPORT:
                    MultiView1.SetActiveView(viewImport);
                    break;
            }
        }

        protected void cmdExport_Click(object sender, EventArgs e)
        {
            var id = DataHelper.GetId(cboMenus.SelectedValue);
            if (id > 0)
            {
                var item = MenuEntity.Provider.Get(id);
                if (item != null)
                {
                    MenuPartDataManager manager = new MenuPartDataManager();

                    manager.GetMenus().Add(item.Id, item);

                    var output = manager.ExportData();

                    var webPath = WebHelper.CombineAddress(WConfig.TempFolder, string.Format("Menu-XML-{0:yyyyMMdd}.xml", DateTime.Now));
                    var path = WebHelper.MapPath(webPath);

                    FileHelper.WriteFile(output, path);

                    WebHelper.DownloadFile(path, string.Empty, false);
                }
            }
        }

        protected void cmdImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                var fileName = FileUpload1.FileName;
                var webPath = WebHelper.CombineAddress(WConfig.TempFolder, fileName);
                var path = WebHelper.MapPath(webPath);

                FileUpload1.PostedFile.SaveAs(path);

                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                MenuPartDataManager manager = new MenuPartDataManager();

                manager.InitImport(doc);
                manager.ImportData(null);

                lblStatus.Text = "Import completed successfully!";

                cboMenus.DataBind();
            }
        }
    }
}