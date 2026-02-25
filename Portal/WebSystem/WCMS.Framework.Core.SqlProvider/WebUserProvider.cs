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
        public WebUser Get(string userName)
        {
            using (var r = DbHelper.ExecuteReader("WebUser_Get",
                DbHelper.CreateParameter("@UserName", userName)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser GetByEmail(string email)
        {
            using (var r = DbHelper.ExecuteReader("WebUser_Get",
                DbHelper.CreateParameter("@Email", email)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser GetByEmailId(string emailId)
        {
            using (var r = DbHelper.ExecuteReader("WebUser_Get",
                DbHelper.CreateParameter("@EmailId", emailId)))
            {
                if (r.HasRows && r.Read())
                    return this.From(r);
            }

            return null;
        }

        public WebUser Get(int userId)
        {
            using (var r = DbHelper.ExecuteReader("WebUser_Get",
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
            using (var r = DbHelper.ExecuteReader("WebUser_Get"))
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
            using (var r = DbHelper.ExecuteReader("WebUser_Get",
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
            object o = DbHelper.ExecuteScalar("WebUser_Set",
                DbHelper.CreateParameter("@UserId", item.Id),
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
                DbHelper.CreateParameter("@Status", item.Status),
                DbHelper.CreateParameter("@MaritalStatusId", item.MaritalStatusId),
                DbHelper.CreateParameter("@LastLoginFailureDate", item.LastLoginFailureDate),
                DbHelper.CreateParameter("@LoginFailureCount", item.LoginFailureCount)
            );

            item.Id = DataUtil.GetId(o);
            return item.Id;
        }

        public bool Delete(int userId)
        {
            DbHelper.ExecuteNonQuery("WebUser_Del",
                DbHelper.CreateParameter("@UserId", userId)
            );
            return true;
        }

        public bool Delete(string userName)
        {
            DbHelper.ExecuteNonQuery("WebUser_Del",
                DbHelper.CreateParameter("@UserName", userName));
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
