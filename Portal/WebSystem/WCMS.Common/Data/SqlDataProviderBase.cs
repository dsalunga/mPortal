using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using WCMS.Common.Utilities;

namespace WCMS.Common.Data
{
    public class DataObject
    {
        public int Id { get; set; }
    }

    public class NamedDataObject : DataObject
    {
        public string Name { get; set; }
    }

    public abstract class SqlDataProviderBase<T> : IDataProvider<T> where T : DataObject
    {
        protected virtual string ConnectionString { get { return SqlHelper.ConnString; } }
        protected abstract string SelectProcedure { get; }
        protected abstract string DeleteProcedure { get; }
        protected virtual string IdParameter { get { return "@Id"; } }

        protected abstract T From(IDataReader r);

        #region IDataProvider<T> Members

        public virtual bool Delete(int id)
        {
            SqlHelper.ExecuteScalar(ConnectionString, DeleteProcedure,
                new SqlParameter(IdParameter, id));

            return true;
        }

        public virtual T Get(int id)
        {
            using (IDataReader r = SqlHelper.ExecuteReader(ConnectionString, SelectProcedure,
                new SqlParameter(IdParameter, id)))
            {
                if (r.Read())
                    return From(r);
            }

            return default(T);
        }

        public virtual T Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetList()
        {
            List<T> items = new List<T>();

            using (IDataReader r = SqlHelper.ExecuteReader(ConnectionString, SelectProcedure))
            {
                while (r.Read())
                    items.Add(From(r));
            }

            return items;
        }

        public virtual List<T> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public virtual int GetCount()
        {
            return GetList().Count;
        }

        public abstract int Update(T item);

        #endregion
    }
}
