using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebUserProvider : IWebUserProvider
    {
        public WebUser Get(string userName)
        {
            using (var r = SqlHelper.ExecuteReader("WebUser_Get",
                new SqlParameter("@UserName", userName)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser GetByEmail(string email)
        {
            using (var r = SqlHelper.ExecuteReader("WebUser_Get",
                new SqlParameter("@Email", email)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser GetByEmailId(string emailId)
        {
            using (var r = SqlHelper.ExecuteReader("WebUser_Get",
                new SqlParameter("@EmailId", emailId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser Get(int userId)
        {
            using (var r = SqlHelper.ExecuteReader("WebUser_Get",
                new SqlParameter("@UserId", userId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public IEnumerable<WebUser> GetList()
        {
            var items = new List<WebUser>();
            using (var r = SqlHelper.ExecuteReader("WebUser_Get"))
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
            using (var r = SqlHelper.ExecuteReader("WebUser_Get",
                new SqlParameter("@Active", active)))
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
            object o = SqlHelper.ExecuteScalar("WebUser_Set",
                new SqlParameter("@UserId", item.Id),
                new SqlParameter("@UserName", item.UserName),
                new SqlParameter("@Password", encryptedPwd),
                new SqlParameter("@FirstName", item.FirstName),
                new SqlParameter("@MiddleName", item.MiddleName),
                new SqlParameter("@LastName", item.LastName),
                new SqlParameter("@Email", item.Email),
                new SqlParameter("@ActivationKey", item.ActivationKey),
                new SqlParameter("@LastUpdate", item.LastUpdate),
                new SqlParameter("@DateCreated", item.DateCreated),
                new SqlParameter("@NewEmail", item.NewEmail),
                new SqlParameter("@Email2", item.Email2),
                new SqlParameter("@Gender", item.Gender),
                new SqlParameter("@NameSuffix", item.NameSuffix),
                new SqlParameter("@MobileNumber", item.MobileNumber),
                new SqlParameter("@TelephoneNumber", item.TelephoneNumber),
                new SqlParameter("@LastLogin", item.LastLogin),
                new SqlParameter("@StatusText", item.StatusText),
                new SqlParameter("@PasswordExpiryDate", item.PasswordExpiryDate),
                new SqlParameter("@PhotoPath", item.PhotoPath),
                new SqlParameter("@ProviderId", item.ProviderId),
                new SqlParameter("@Status", item.Status),
                new SqlParameter("@MaritalStatusId", item.MaritalStatusId),
                new SqlParameter("@LastLoginFailureDate", item.LastLoginFailureDate),
                new SqlParameter("@LoginFailureCount", item.LoginFailureCount)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int userId)
        {
            SqlHelper.ExecuteNonQuery("WebUser_Del",
                new SqlParameter("@UserId", userId)
            );
            return true;
        }

        public bool Delete(string userName)
        {
            SqlHelper.ExecuteNonQuery("WebUser_Del",
                new SqlParameter("@UserName", userName));
            return true;
        }

        private WebUser From(DbDataReader r)
        {
            string pwd = DataUtil.Get(r, "Password");
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
            item.MaritalStatusId = DataUtil.GetId(r, "MaritalStatusId");
            item.LastLoginFailureDate = DataUtil.GetDateTime(r, "LastLoginFailureDate");
            item.LoginFailureCount = DataUtil.GetInt32(r, "LoginFailureCount");
            return item;
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
