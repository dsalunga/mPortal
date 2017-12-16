using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.GenericForm
{
    public class GenericListPresenter
    {
        private DataSet _ds;
        private List<NamedValueProvider> _list;
        //private List<DataColumn> _schemaColumns;

        public GenericListPresenter(int listId)
        {
            _ds = SqlHelper.ExecuteDataSet("GenericListRow_Get",
                new SqlParameter("@ListId", listId));
        }

        public DataSet DataSet
        {
            get { return null; }
        }

        public List<NamedValueProvider> Rows
        {
            get
            {
                if (_list == null)
                {
                    _list = new List<NamedValueProvider>();
                }

                return _list;
            }
        }
    }
}
