using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebUserProvider : IWebUserProvider
    {
        private static readonly object ColumnCacheSync = new object();
        private static HashSet<string> _tableColumns;

        public WebUser Get(string userName)
        {
            var sql = "SELECT * FROM WebUser WHERE " + DbSyntax.QuoteIdentifier("UserName") + " = @UserName";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserName", userName)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser GetByEmail(string email)
        {
            var sql = "SELECT * FROM WebUser WHERE " + DbSyntax.QuoteIdentifier("Email") + " = @Email";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Email", email)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser GetByEmailId(string emailId)
        {
            var sql = "SELECT * FROM WebUser WHERE LOWER(" + DbSyntax.QuoteIdentifier("Email") + ") LIKE @EmailPattern";
            var emailPattern = (emailId ?? string.Empty).Trim().ToLowerInvariant() + "@%";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@EmailPattern", emailPattern)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser Get(int userId)
        {
            var sql = "SELECT * FROM WebUser WHERE " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserId", userId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebUser> GetList()
        {
            var items = new List<WebUser>();
            var sql = "SELECT * FROM WebUser";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public IEnumerable<WebUser> GetList(int active)
        {
            var items = new List<WebUser>();
            var sql = "SELECT * FROM WebUser WHERE " + DbSyntax.QuoteIdentifier("Active") + " = @Active";
            using (var r = DbHelper.ExecuteReader(CommandType.Text, sql,
                DbHelper.CreateParameter("@Active", active)))
            {
                if (r.HasRows)
                    while (r.Read())
                        items.Add(this.From(r));
            }

            return items;
        }

        public int Update(WebUser item)
        {
            string encryptedPwd = !string.IsNullOrEmpty(item.Password) ? WCryptography.EncryptString(item.Password) : string.Empty;
            string sql;
            List<DbParameter> parms;
            var includeMaritalStatusId = HasTableColumn("MaritalStatusId");
            var includeLastLoginFailureDate = HasTableColumn("LastLoginFailureDate");
            var includeLoginFailureCount = HasTableColumn("LoginFailureCount");

            if (item.Id > 0)
            {
                var setParts = new List<string>
                {
                    DbSyntax.QuoteIdentifier("UserName") + " = @UserName",
                    DbSyntax.QuoteIdentifier("Password") + " = @Password",
                    DbSyntax.QuoteIdentifier("FirstName") + " = @FirstName",
                    DbSyntax.QuoteIdentifier("MiddleName") + " = @MiddleName",
                    DbSyntax.QuoteIdentifier("LastName") + " = @LastName",
                    DbSyntax.QuoteIdentifier("Email") + " = @Email",
                    DbSyntax.QuoteIdentifier("ActivationKey") + " = @ActivationKey",
                    DbSyntax.QuoteIdentifier("LastUpdate") + " = @LastUpdate",
                    DbSyntax.QuoteIdentifier("DateCreated") + " = @DateCreated",
                    DbSyntax.QuoteIdentifier("NewEmail") + " = @NewEmail",
                    DbSyntax.QuoteIdentifier("Email2") + " = @Email2",
                    DbSyntax.QuoteIdentifier("Gender") + " = @Gender",
                    DbSyntax.QuoteIdentifier("NameSuffix") + " = @NameSuffix",
                    DbSyntax.QuoteIdentifier("MobileNumber") + " = @MobileNumber",
                    DbSyntax.QuoteIdentifier("TelephoneNumber") + " = @TelephoneNumber",
                    DbSyntax.QuoteIdentifier("LastLogin") + " = @LastLogin",
                    DbSyntax.QuoteIdentifier("StatusText") + " = @StatusText",
                    DbSyntax.QuoteIdentifier("PasswordExpiryDate") + " = @PasswordExpiryDate",
                    DbSyntax.QuoteIdentifier("PhotoPath") + " = @PhotoPath",
                    DbSyntax.QuoteIdentifier("ProviderId") + " = @ProviderId",
                    DbSyntax.QuoteIdentifier("Status") + " = @Status"
                };

                if (includeMaritalStatusId)
                    setParts.Add(DbSyntax.QuoteIdentifier("MaritalStatusId") + " = @MaritalStatusId");
                if (includeLastLoginFailureDate)
                    setParts.Add(DbSyntax.QuoteIdentifier("LastLoginFailureDate") + " = @LastLoginFailureDate");
                if (includeLoginFailureCount)
                    setParts.Add(DbSyntax.QuoteIdentifier("LoginFailureCount") + " = @LoginFailureCount");

                sql = "UPDATE WebUser SET " +
                    string.Join(", ", setParts) +
                    " WHERE " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId";
                parms = new List<DbParameter>
                {
                    DbHelper.CreateParameter("@UserName", item.UserName),
                    DbHelper.CreateParameter("@Password", encryptedPwd),
                    DbHelper.CreateParameter("@FirstName", item.FirstName),
                    DbHelper.CreateParameter("@MiddleName", item.MiddleName),
                    DbHelper.CreateParameter("@LastName", item.LastName),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@ActivationKey", item.ActivationKey),
                    DbHelper.CreateParameter("@LastUpdate", item.LastUpdate),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@NewEmail", item.NewEmail),
                    DbHelper.CreateParameter("@Email2", item.Email2),
                    DbHelper.CreateParameter("@Gender", item.Gender),
                    DbHelper.CreateParameter("@NameSuffix", item.NameSuffix),
                    DbHelper.CreateParameter("@MobileNumber", item.MobileNumber),
                    DbHelper.CreateParameter("@TelephoneNumber", item.TelephoneNumber),
                    DbHelper.CreateParameter("@LastLogin", item.LastLogin),
                    DbHelper.CreateParameter("@StatusText", item.StatusText),
                    DbHelper.CreateParameter("@PasswordExpiryDate", item.PasswordExpiryDate),
                    DbHelper.CreateParameter("@PhotoPath", item.PhotoPath),
                    DbHelper.CreateParameter("@ProviderId", item.ProviderId),
                    DbHelper.CreateParameter("@Status", item.Status)
                };

                if (includeMaritalStatusId)
                    parms.Add(DbHelper.CreateParameter("@MaritalStatusId", item.MaritalStatusId));
                if (includeLastLoginFailureDate)
                    parms.Add(DbHelper.CreateParameter("@LastLoginFailureDate", item.LastLoginFailureDate));
                if (includeLoginFailureCount)
                    parms.Add(DbHelper.CreateParameter("@LoginFailureCount", item.LoginFailureCount));

                parms.Add(DbHelper.CreateParameter("@UserId", item.Id));
                DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms.ToArray());
            }
            else
            {
                var insertColumns = new List<string>
                {
                    DbSyntax.QuoteIdentifier("UserName"),
                    DbSyntax.QuoteIdentifier("Password"),
                    DbSyntax.QuoteIdentifier("FirstName"),
                    DbSyntax.QuoteIdentifier("MiddleName"),
                    DbSyntax.QuoteIdentifier("LastName"),
                    DbSyntax.QuoteIdentifier("Email"),
                    DbSyntax.QuoteIdentifier("ActivationKey"),
                    DbSyntax.QuoteIdentifier("LastUpdate"),
                    DbSyntax.QuoteIdentifier("DateCreated"),
                    DbSyntax.QuoteIdentifier("NewEmail"),
                    DbSyntax.QuoteIdentifier("Email2"),
                    DbSyntax.QuoteIdentifier("Gender"),
                    DbSyntax.QuoteIdentifier("NameSuffix"),
                    DbSyntax.QuoteIdentifier("MobileNumber"),
                    DbSyntax.QuoteIdentifier("TelephoneNumber"),
                    DbSyntax.QuoteIdentifier("LastLogin"),
                    DbSyntax.QuoteIdentifier("StatusText"),
                    DbSyntax.QuoteIdentifier("PasswordExpiryDate"),
                    DbSyntax.QuoteIdentifier("PhotoPath"),
                    DbSyntax.QuoteIdentifier("ProviderId"),
                    DbSyntax.QuoteIdentifier("Status")
                };
                var insertValues = new List<string>
                {
                    "@UserName",
                    "@Password",
                    "@FirstName",
                    "@MiddleName",
                    "@LastName",
                    "@Email",
                    "@ActivationKey",
                    "@LastUpdate",
                    "@DateCreated",
                    "@NewEmail",
                    "@Email2",
                    "@Gender",
                    "@NameSuffix",
                    "@MobileNumber",
                    "@TelephoneNumber",
                    "@LastLogin",
                    "@StatusText",
                    "@PasswordExpiryDate",
                    "@PhotoPath",
                    "@ProviderId",
                    "@Status"
                };

                if (includeMaritalStatusId)
                {
                    insertColumns.Add(DbSyntax.QuoteIdentifier("MaritalStatusId"));
                    insertValues.Add("@MaritalStatusId");
                }
                if (includeLastLoginFailureDate)
                {
                    insertColumns.Add(DbSyntax.QuoteIdentifier("LastLoginFailureDate"));
                    insertValues.Add("@LastLoginFailureDate");
                }
                if (includeLoginFailureCount)
                {
                    insertColumns.Add(DbSyntax.QuoteIdentifier("LoginFailureCount"));
                    insertValues.Add("@LoginFailureCount");
                }

                sql = "INSERT INTO WebUser (" +
                    string.Join(", ", insertColumns) +
                    ") VALUES (" +
                    string.Join(", ", insertValues) +
                    ")";
                if (DbHelper.Provider == DatabaseProvider.PostgreSql)
                    sql += " RETURNING " + DbSyntax.QuoteIdentifier("UserId");
                else
                    sql += "; SELECT SCOPE_IDENTITY()";
                parms = new List<DbParameter>
                {
                    DbHelper.CreateParameter("@UserName", item.UserName),
                    DbHelper.CreateParameter("@Password", encryptedPwd),
                    DbHelper.CreateParameter("@FirstName", item.FirstName),
                    DbHelper.CreateParameter("@MiddleName", item.MiddleName),
                    DbHelper.CreateParameter("@LastName", item.LastName),
                    DbHelper.CreateParameter("@Email", item.Email),
                    DbHelper.CreateParameter("@ActivationKey", item.ActivationKey),
                    DbHelper.CreateParameter("@LastUpdate", item.LastUpdate),
                    DbHelper.CreateParameter("@DateCreated", item.DateCreated),
                    DbHelper.CreateParameter("@NewEmail", item.NewEmail),
                    DbHelper.CreateParameter("@Email2", item.Email2),
                    DbHelper.CreateParameter("@Gender", item.Gender),
                    DbHelper.CreateParameter("@NameSuffix", item.NameSuffix),
                    DbHelper.CreateParameter("@MobileNumber", item.MobileNumber),
                    DbHelper.CreateParameter("@TelephoneNumber", item.TelephoneNumber),
                    DbHelper.CreateParameter("@LastLogin", item.LastLogin),
                    DbHelper.CreateParameter("@StatusText", item.StatusText),
                    DbHelper.CreateParameter("@PasswordExpiryDate", item.PasswordExpiryDate),
                    DbHelper.CreateParameter("@PhotoPath", item.PhotoPath),
                    DbHelper.CreateParameter("@ProviderId", item.ProviderId),
                    DbHelper.CreateParameter("@Status", item.Status)
                };
                if (includeMaritalStatusId)
                    parms.Add(DbHelper.CreateParameter("@MaritalStatusId", item.MaritalStatusId));
                if (includeLastLoginFailureDate)
                    parms.Add(DbHelper.CreateParameter("@LastLoginFailureDate", item.LastLoginFailureDate));
                if (includeLoginFailureCount)
                    parms.Add(DbHelper.CreateParameter("@LoginFailureCount", item.LoginFailureCount));

                var o = DbHelper.ExecuteScalar(CommandType.Text, sql, parms.ToArray());
                item.Id = DataUtil.GetId(o);
            }

            return item.Id;
        }

        public bool Delete(int userId)
        {
            var sql = "DELETE FROM WebUser WHERE " + DbSyntax.QuoteIdentifier("UserId") + " = @UserId";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserId", userId)
            );
            return true;
        }

        public bool Delete(string userName)
        {
            var sql = "DELETE FROM WebUser WHERE " + DbSyntax.QuoteIdentifier("UserName") + " = @UserName";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                DbHelper.CreateParameter("@UserName", userName));
            return true;
        }

        private WebUser From(DbDataReader r)
        {
            string pwd = DataUtil.Get(r, "Password");
            var hasMaritalStatusId = HasColumn(r, "MaritalStatusId");
            var hasLastLoginFailureDate = HasColumn(r, "LastLoginFailureDate");
            var hasLoginFailureCount = HasColumn(r, "LoginFailureCount");
            var item = new WebUser();
            item.Id = DataUtil.GetId(r, WebColumns.UserId);
            item.UserName = DataUtil.Get(r, WebColumns.UserName);
            item.Password = !string.IsNullOrEmpty(pwd) ? WCryptography.DecryptString(pwd) : string.Empty;
            item.FirstName = DataUtil.Get(r, WebColumns.FirstName);
            item.MiddleName = DataUtil.Get(r, WebColumns.MiddleName);
            item.LastName = DataUtil.Get(r, WebColumns.LastName);
            item.Email = DataUtil.Get(r, WebColumns.Email);
            item.DateCreated = DataUtil.GetDateTime(r, "DateCreated");
            item.LastUpdate = DataUtil.GetDateTime(r, "LastUpdate");
            item.ActivationKey = DataUtil.Get(r,"ActivationKey");
            item.NewEmail = DataUtil.Get(r, "NewEmail");
            item.Email2 = DataUtil.Get(r, "Email2");
            item.Gender = DataUtil.GetChar(r, "Gender");
            item.NameSuffix = DataUtil.Get(r, "NameSuffix");
            item.MobileNumber = DataUtil.Get(r, "MobileNumber");
            item.TelephoneNumber = DataUtil.Get(r, "TelephoneNumber");
            item.LastLogin = DataUtil.GetDateTime(r, "LastLogin");
            item.StatusText = DataUtil.Get(r, "StatusText");
            item.PasswordExpiryDate = DataUtil.GetDateTime(r, "PasswordExpiryDate");
            item.PhotoPath = DataUtil.Get(r, "PhotoPath");
            item.ProviderId = DataUtil.GetId(r, "ProviderId");
            item.Status = DataUtil.GetInt32(r, WebColumns.Status);
            item.MaritalStatusId = hasMaritalStatusId ? DataUtil.GetId(r, "MaritalStatusId") : -1;
            item.LastLoginFailureDate = hasLastLoginFailureDate ? DataUtil.GetDateTime(r, "LastLoginFailureDate") : WConstants.DateTimeMinValue;
            item.LoginFailureCount = hasLoginFailureCount ? DataUtil.GetInt32(r, "LoginFailureCount") : 0;
            return item;
        }

        private static bool HasColumn(DbDataReader reader, string columnName)
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static bool HasTableColumn(string columnName)
        {
            EnsureTableColumns();
            return _tableColumns != null && _tableColumns.Contains(columnName);
        }

        private static void EnsureTableColumns()
        {
            if (_tableColumns != null)
                return;

            lock (ColumnCacheSync)
            {
                if (_tableColumns != null)
                    return;

                var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (var reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM WebUser WHERE 1 = 0"))
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                        columns.Add(reader.GetName(i));
                }

                _tableColumns = columns;
            }
        }

        #region IDataProvider<WebUser> Members


        public WebUser Get(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WebUser> GetList(params QueryFilterElement[] filters)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return GetList().Count();
        }

        #endregion

        public IEnumerable<WebDirectoryEntry> GetByDirectory(int directoryId, string loweredKeyword)
        {
            throw new NotImplementedException();
        }


        public WebUser Refresh(WebUser item)
        {
            throw new NotImplementedException();
        }
    }
}
