
namespace WCMS.Framework.Core
{
    public class WebFile : ParameterizedWebObject, ISelfManager
    {
        protected static IWebFileProvider _provider;

        static WebFile()
        {
            _provider = DataAccess.CreateProvider<IWebFileProvider>();
        }

        public WebFile()
        {
            Id = -1;
            FolderId = -1;
            ObjectId = -1;
            RecordId = -1;
        }

        public int FolderId { get; set; }
        public int ObjectId { get; set; }
        public int RecordId { get; set; }

        public static IWebFileProvider Provider
        {
            get { return _provider; }
        }

        #region IWebObject Members

        public override int OBJECT_ID
        {
            get { return WebObjects.WebFile; }
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

        public bool Refresh()
        {
            _provider.Refresh(this);

            return true;
        }

        #endregion
    }
}
