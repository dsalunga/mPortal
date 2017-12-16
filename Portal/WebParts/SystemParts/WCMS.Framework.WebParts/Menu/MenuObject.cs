using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Menu.Providers;

namespace WCMS.WebSystem.WebParts.Menu
{
    public class MenuObject : WebObjectBase, ISelfManager
    {
        private static IMenuObjectProvider _manager;

        static MenuObject()
        {
            _manager = WebObject.ResolveManager<MenuObject, IMenuObjectProvider>(WebObject.ResolveProvider<MenuObject, IMenuObjectProvider>());
        }

        public MenuObject()
        {
            RecordId = -1;
            ObjectId = -1;
            MenuId = -1;
            ParameterSetId = -1;
            RenderMode = 0;

            Width = 0;
            Height = 0;
            Horizontal = 0;
        }

        public int RecordId { get; set; }
        public int ObjectId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Horizontal { get; set; }
        public int MenuId { get; set; }
        public int ParameterSetId { get; set; }
        public int RenderMode { get; set; }

        public override int OBJECT_ID { get { return 61; } }

        public MenuEntity GetMenu()
        {
            return MenuId > 0 ? MenuEntity.Provider.Get(MenuId) : null;
        }

        public static IMenuObjectProvider Provider
        {
            get { return _manager; }
        }

        #region ISelfManager Members

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        #endregion
    }
}
