using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCMS.Framework.Core;

namespace WCMS.Framework.Security
{
    /// <summary>
    /// If not null, login is successful
    /// </summary>
    public class ExternalLoginResult
    {
        public Dictionary<string, object> Properties { get; set; }
        public LoginCodes StatusCode { get; set; }

        public ExternalLoginResult()
        {
            Properties = new Dictionary<string, object>();
            StatusCode = LoginCodes.NULL;
        }
    }

    public enum LoginCodes
    {
        NULL = -1,
        Success = 0,
        Failed= 1,
        NonExisting = 2
    }

    public class UserProvider : NamedWebObject, ISelfManager
    {
        private static IUserProviderProvider _manager;

        static UserProvider()
        {
            _manager = WebObject.ResolveManager<UserProvider, IUserProviderProvider>(WebObject.ResolveProvider<UserProvider, IUserProviderProvider>());
        }

        public static IUserProviderProvider Provider { get { return _manager; } }

        public UserProvider()
        {

        }

        public string ProviderName { get; set; }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        private IUserProvider _provider = null;
        public IUserProvider ResolveProvider()
        {
            if (_provider == null && !string.IsNullOrEmpty(ProviderName))
                _provider = Activator.CreateInstance(Type.GetType(ProviderName)) as IUserProvider;

            return _provider;
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }
    }
}
