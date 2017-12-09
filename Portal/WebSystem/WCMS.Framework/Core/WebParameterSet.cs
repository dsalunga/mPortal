using System.Linq;

namespace WCMS.Framework.Core
{
    public class WebParameterSet : ParameterizedWebObject, ISelfManager
    {
        public const string SCHEMAS = "SCHEMAS";

        public string SchemaParameterName { get; set; }

        private static IWebParameterSetProvider _manager;

        public WebParameterSet()
        {
            SchemaParameterName = string.Empty;
        }

        static WebParameterSet()
        {
            _manager = WebObject.ResolveManager<WebParameterSet, IWebParameterSetProvider>(WebObject.ResolveProvider<WebParameterSet, IWebParameterSetProvider>());
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebParameterSet; }
        }

        public static IWebParameterSetProvider Provider
        {
            get { return _manager; }
        }

        public static WebParameterSet Get(string name)
        {
            var items = _manager.GetList();

            var item = items.FirstOrDefault(i => i.Name == name);

            return item;
        }

        public WebParameter GetSchemaParameter()
        {
            WebParameter item = null;

            if (!string.IsNullOrEmpty(SchemaParameterName))
            {
                var set = GetSchemaSet();
                if (set != null)
                    item = set.GetParameter(SchemaParameterName);
            }

            return item;
        }

        public static WebParameterSet GetSchemaSet()
        {
            return WebParameterSet.Get(SCHEMAS);
        }

        #region ISelfManager Members

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            this.DeleteRelatedObjects();

            return _manager.Delete(this.Id);
        }

        #endregion
    }
}
