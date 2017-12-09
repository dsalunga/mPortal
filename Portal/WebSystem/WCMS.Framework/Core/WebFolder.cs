using System.Text;

namespace WCMS.Framework.Core
{
    public class WebFolder : ParameterizedWebObject, ISelfManager
    {
        protected static IWebFolderProvider _provider;

        static WebFolder()
        {
            _provider = DataAccess.CreateProvider<IWebFolderProvider>();
        }

        public WebFolder()
        {
            Id = -1;
            ParentId = -1;
            ObjectId = -1;
            SiteId = -1;
            ShareName = "";
        }

        #region Instance Methods

        public string BuildRelativePath()
        {
            StringBuilder sb = new StringBuilder();

            var item = this;
            while (item != null)
            {
                sb.Insert(0, "/" + item.Name);
                item = item.Parent;
            }

            return sb.ToString();
        }

        #endregion

        #region Properties

        public int ParentId { get; set; }
        public string ShareName { get; set; }

        public int ObjectId { get; set; }
        public int SiteId { get; set; }

        public WebFolder Parent
        {
            get
            {
                if (ParentId > 0)
                    return _provider.Get(ParentId);

                return null;
            }
        }

        public static IWebFolderProvider Provider
        {
            get { return _provider; }
        }

        #endregion

        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebFolder; }
        }

        #endregion

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion

        #region Static Methods

        public static WebFolder SelectNode(string path)
        {
            int parentId = -1;

            string[] nodeNames = path.Split('/');
            for (int i = 0; i < nodeNames.Length - 1; i++)
            {
                string nodeName = nodeNames[i].Trim();
                if (string.IsNullOrEmpty(nodeName)) continue;

                parentId = WebFolder.Provider.Get(parentId, nodeName).Id;
            }

            string lastNode = nodeNames[nodeNames.Length - 1];
            return WebFolder.Provider.Get(parentId, lastNode);
        }

        #endregion
    }
}
