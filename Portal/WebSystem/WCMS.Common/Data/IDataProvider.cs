using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common.Data
{
    public interface IDataProvider<T>
    {
        T Get(int id);
        List<T> GetList();
        int Update(T item);
        bool Delete(int id);
        int GetCount();
    }
}
