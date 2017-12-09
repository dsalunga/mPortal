using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using WCMS.Common.Utilities;
using WCMS.Framework.Core.Interfaces;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebUserRoleProvider : IWebUserRoleProvider
    {
        public WebUserRole Get(int userRoleId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebUserRole_Get",
                new SqlParameter("@UserRoleId", userRoleId)
                ))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public WebUserRole Get(int roleId, int userId)
        {
            using (DbDataReader r = SqlHelper.ExecuteReader("WebUserRole_Get",
                new SqlParameter("@UserId", userId),
                new SqlParameter("@RoleId", roleId)
                ))
            {
                if (r.HasRows && r.Read())
                {
                    return this.From(r);
                }
            }

            return null;
        }

        public List<WebUserRole> Get()
        {
            List<WebUserRole> items = new List<WebUserRole>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebUserRole_Get"))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public List<WebUserRole> GetByUserId(int userId)
        {
            List<WebUserRole> items = new List<WebUserRole>();
            
            using (DbDataReader r = SqlHelper.ExecuteReader("WebUserRole_Get",
                new SqlParameter("@UserId", userId)
                ))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public List<WebUserRole> GetByRoleId(int roleId)
        {
            List<WebUserRole> items = new List<WebUserRole>();

            using (DbDataReader r = SqlHelper.ExecuteReader("WebUserRole_Get",
                new SqlParameter("@RoleId", roleId)
                ))
            {
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        items.Add(this.From(r));
                    }
                }
            }

            return items;
        }

        public int Update(WebUserRole item)
        {
            object o = SqlHelper.ExecuteScalar("WebUserRole_Set",
                new SqlParameter("@UserRoleId", item.Id),
                new SqlParameter("@UserId", item.UserId),
                new SqlParameter("@RoleId", item.RoleId)
                );

            item.Id = DataHelper.GetDbId(o);
            return item.Id;
        }

        public bool Delete(int userRoleId)
        {
            SqlHelper.ExecuteNonQuery("WebUserRole_Del",
                new SqlParameter("@UserRoleId", userRoleId)
                );

            return true;
        }

        private WebUserRole From(DbDataReader r)
        {
            WebUserRole item = new WebUserRole();
            item.Id = DataHelper.GetDbId(r["UserRoleId"]);
            item.RoleId = DataHelper.GetDbId(r["RoleId"]);
            item.UserId = DataHelper.GetDbId(r["UserId"]);

            return item;
        }
    }
}
