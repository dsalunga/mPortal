using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WCMS.WebSystem.Startup))]
namespace WCMS.WebSystem
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
