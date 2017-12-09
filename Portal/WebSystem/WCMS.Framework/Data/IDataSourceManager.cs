using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IDataSourceManager
    {
        T CreateDAL<T>();
        string AssemblyPath { get; set; }
        string DataSourceName { get; set; }
    }
}
