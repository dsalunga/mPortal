using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.GenericForm
{
    public class GenericListPresenter
    {
        private DataSet _ds;
        private List<NamedValueProvider> _list;

        public GenericListPresenter(int listId)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier("GenericListRow") + " WHERE " + DbSyntax.QuoteIdentifier("ListId") + " = @ListId";
            _ds = DbHelper.ExecuteDataSet(CommandType.Text, sql,
                DbHelper.CreateParameter("@ListId", listId));
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
