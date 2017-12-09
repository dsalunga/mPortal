using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

namespace WCMS.Framework
{
    public class PartDataManagerModel
    {
        public PartDataManagerModel()
        {

        }

        public PartDataManagerModel(int partId, int partControlId, string typeName)
        {
            this.PartId = partId;
            this.PartControlId = partControlId;
            this.TypeName = typeName;
        }

        public int PartId { get; set; }
        public int PartControlId { get; set; }
        public string TypeName { get; set; }

        private IPartDataManager _manager = null;
        public IPartDataManager GetManager()
        {
            if (_manager == null && !string.IsNullOrEmpty(TypeName))
                _manager = ReflectionUtil.LoadAndCreateInstance<IPartDataManager>(TypeName);

            return _manager;
        }
    }
}
