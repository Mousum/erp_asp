using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mhasb.Wsit.Web.Admin.Startup))]
namespace Mhasb.Wsit.Web.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
