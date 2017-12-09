using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Agent
{
    public static class AgentConfig
    {
        static AgentConfig()
        {
            WebRegistry.Updated += RegistryNodeUpdated;
        }

        private static void RegistryNodeUpdated(object sender, WebRegistryUpdateEventArgs e)
        {
            var id = e.UpdatedNode.Id;
            if (_execPathId != -1 && id == _execPathId)
            {
                _execPath = null;
                var value = ExecutablePath;
            }
            else if (_name != null && id == _name.Id)
            {
                _name = null;
                var value = Name;
            }
            else if (_processName != null && id == _processName.Id)
            {
                _processName = null;
                var value = ProcessName;
            }
        }

        private static string _execPath;
        private static int _execPathId = -1;
        public static string ExecutablePath
        {
            get
            {
                if (_execPath == null)
                {
                    var item = WebRegistry.SelectNode("/System/Agent/ExecutablePath");
                    if (item != null)
                    {
                        _execPath = WebHelper.MapPath(item.Value, true);
                        _execPathId = item.Id;
                    }
                    else
                    {
                        _execPath = string.Empty;
                    }
                }
                return _execPath;
            }
        }


        private static WebRegistry _processName;
        public static string ProcessName
        {
            get
            {
                if (_processName == null)
                {
                    _processName = WebRegistry.SelectNode("/System/Agent/ProcessName");
                    if (_processName == null)
                        return string.Empty;
                }
                return _processName.Value;
            }
        }

        private static WebRegistry _name;
        public static string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = WebRegistry.SelectNode("/System/Agent/Name");
                    if (_name == null)
                        return string.Empty;
                }
                return _name.Value;
            }
        }
    }
}
