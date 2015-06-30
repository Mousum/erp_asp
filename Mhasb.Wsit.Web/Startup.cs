using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mhasb.Wsit.Web.Startup))]
namespace Mhasb.Wsit.Web
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {    
            ConfigureAuth(app);
        }
    }
}
