using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

using WCMS.Framework.Core;

namespace WCMS.Framework.Core
{
    public abstract class GenericSqlDataProviderBase<T> : GenericSqlDataProvider<T>, IDataProvider<T> where T : IWebObject
    {
        protected abstract string SelectProcedure { get; }
        protected abstract string DeleteProcedure { get; }
        protected virtual string IdParameter { get { return "@Id"; } }

        protected virtual T From(IDataReader r)
        {
            return From(r, default(T));
        }

        protected abstract T From(IDataReader r, T source);

        public virtual T Refresh(T item)
        {
            using (IDataReader r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter(IdParameter, item.Id)))
            {
                if (r.Read())
                    return From(r, item);
            }

            return default(T);
        }

        #region IDataProvider<T> Members

        public new virtual bool Delete(int id)
        {
            SqlHelper.ExecuteNonQuery(DeleteProcedure,
                new SqlParameter(IdParameter, id));

            return true;
        }

        public new virtual T Get(int id)
        {
            using (IDataReader r = SqlHelper.ExecuteReader(SelectProcedure,
                new SqlParameter(IdParameter, id)))
            {
                if (r.Read())
                    return From(r);
            }

            return default(T);
        }

        public new virtual IEnumerable<T> GetList()
        {
            List<T> items = new List<T>();

            using (IDataReader r = SqlHelper.ExecuteReader(SelectProcedure))
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

            item.Id = DataHelper.GetId(postUpdateResult);

            if (!isUpdate)
                WebObject.OnRecordCreated(item);

            return item.Id;
        }

        #endregion
    }
}
