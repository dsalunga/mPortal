using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public abstract class GenericSqlDataProviderBase<T> : GenericSqlDataProvider<T>, IDataProvider<T> where T : IWebObject
    {
        protected abstract string TableName { get; }
        protected abstract string IdColumn { get; }

        protected virtual string SelectProcedure { get { return TableName + "_Get"; } }
        protected virtual string DeleteProcedure { get { return TableName + "_Del"; } }
        protected virtual string IdParameter { get { return "@Id"; } }

        protected virtual T From(IDataReader r)
        {
            return From(r, default(T));
        }

        protected abstract T From(IDataReader r, T source);

        public virtual T Refresh(T item)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier(TableName) + " WHERE " + DbSyntax.QuoteIdentifier(IdColumn) + " = @" + IdColumn;
            using (IDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@" + IdColumn, item.Id)))
            {
                if (r.Read())
                    return From(r, item);
            }

            return default(T);
        }

        #region IDataProvider<T> Members

        public new virtual bool Delete(int id)
        {
            var sql = "DELETE FROM " + DbSyntax.QuoteIdentifier(TableName) + " WHERE " + DbSyntax.QuoteIdentifier(IdColumn) + " = @" + IdColumn;
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@" + IdColumn, id));

            return true;
        }

        public new virtual T Get(int id)
        {
            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier(TableName) + " WHERE " + DbSyntax.QuoteIdentifier(IdColumn) + " = @" + IdColumn;
            using (IDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@" + IdColumn, id)))
            {
                if (r.Read())
                    return From(r);
            }

            return default(T);
        }

        public new virtual IEnumerable<T> GetList()
        {
            List<T> items = new List<T>();

            var sql = "SELECT * FROM " + DbSyntax.QuoteIdentifier(TableName);
            using (IDataReader r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        //public abstract int Update(T item);

        protected int UpdatePostProcess(T item, object postUpdateResult)
        {
            bool isUpdate = item.Id > 0;

            item.Id = DataUtil.GetId(postUpdateResult);

            if (!isUpdate)
                WebObject.OnRecordCreated(item);

            return item.Id;
        }

        #endregion
    }
}
