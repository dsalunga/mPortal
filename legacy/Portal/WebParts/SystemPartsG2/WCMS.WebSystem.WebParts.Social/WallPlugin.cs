using System;
using WCMS.Framework.Core;
using WCMS.Framework.Social.Providers;

namespace WCMS.Framework.Social
{
    public class WallPlugin : NamedWebObject, ISelfManager
    {
        private static IWallPluginProvider _provider;

        static WallPlugin()
        {
            _provider = WebObject.ResolveManager<WallPlugin, IWallPluginProvider>(WebObject.ResolveProvider<WallPlugin, IWallPluginProvider>());
        }

        public WallPlugin()
        {
            EventTypeId = -1;
            FileName = string.Empty;
            TypeName = string.Empty;
        }

        public int EventTypeId { get; set; }
        public string FileName { get; set; }
        public string TypeName { get; set; }

        private Type _typeObject;
        public Type TypeObject
        {
            get
            {
                if (_typeObject == null && !string.IsNullOrEmpty(TypeName))
                    _typeObject = Type.GetType(TypeName);

                return _typeObject;
            }
        }

        public static IWallPluginProvider Provider { get { return _provider; } }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public override int OBJECT_ID
        {
            get { return SocialConstants.WallPlugin; }
        }
    }
}
