using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Danterest2.Web.Startup))]
namespace Danterest2.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
