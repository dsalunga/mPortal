using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class SaveInFolder : System.Web.UI.UserControl, WCMS.WebSystem.Controls.ISaveInFolder
    {
        public SaveInFolder()
        {
            ObjectId = -1;
            //SingleInstance = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public int ObjectId { get; set; }

        //public bool SingleInstance { get; set; }

        public string SelectedPath
        {
            set { txtFolder.Text = value; }

            get { return txtFolder.Text.Trim(); }
        }

        public int FolderId
        {
            get
            {
                if (!string.IsNullOrEmpty(SelectedPath))
                {
                    WebFolder item = WebFolder.SelectNode(SelectedPath);
                    if (item != null)
                        return item.Id;

                    return -1;
                }

                return -2;
            }

            set
            {
                if (value > 0)
                {
                    WebFolder item = WebFolder.Provider.Get(value);
                    if (item != null)
                        SelectedPath = item.BuildRelativePath();
                }
                else
                {
                    SelectedPath = string.Empty;
                }
            }
        }

        public int FolderTextBoxSize
        {
            get { return txtFolder.Columns; }
            set { txtFolder.Columns = value; }
        }

        public void Update(string name, int objectId, int recordId)
        {
            var folderId = FolderId;
            WebFile file = null;

            var items = WebFile.Provider.GetList(objectId, recordId);
            if (items.Count() > 0)
            {
                file = items.First();
                if (items.Count() > 1)
                {
                    for (int i = 1; i < items.Count(); i++)
                    {
                        items.ElementAt(i).Delete();
                    }
                }
            }

            //file = WebFile.Provider.Get(folderId, objectId, recordId);
            if (file == null)
            {
                file = new WebFile();
                file.ObjectId = objectId;
                file.RecordId = recordId;
            }

            file.FolderId = folderId;
            file.Name = name;
            file.Update();
        }

        public int GetFolder(int objectId, int recordId)
        {
            WebFile item = WebFile.Provider.Get(objectId, recordId);
            if (item != null)
            {
                return item.FolderId;
            }

            return -1;
        }
    }
}