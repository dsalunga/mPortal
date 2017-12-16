using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.GenericList.Providers
{
    public class GenericListFieldProvider : IGenericListFieldProvider
    {

        #region IDataProvider<GenericListField> Members

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public GenericListField Get(int id)
        {
            throw new NotImplementedException();
        }

        public GenericListField Get(params WCMS.Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenericListField> GetList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenericListField> GetList(params WCMS.Framework.Core.QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public int Update(GenericListField item)
        {
            throw new NotImplementedException();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public GenericListField Refresh(GenericListField item)
        {
            throw new NotImplementedException();
        }
    }
}
