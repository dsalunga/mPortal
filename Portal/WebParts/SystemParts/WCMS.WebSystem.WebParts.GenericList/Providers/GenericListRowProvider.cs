using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.GenericList.Providers
{
    public class GenericListRowProvider : IGenericListRowProvider
    {
        #region IDataProvider<GenericListRow> Members

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public GenericListRow Get(int id)
        {
            throw new NotImplementedException();
        }

        public GenericListRow Get(params WCMS.Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenericListRow> GetList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenericListRow> GetList(params WCMS.Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public int Update(GenericListRow item)
        {
            throw new NotImplementedException();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public GenericListRow Refresh(GenericListRow item)
        {
            throw new NotImplementedException();
        }
    }
}
