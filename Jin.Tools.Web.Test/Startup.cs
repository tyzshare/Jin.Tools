using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jin.Tools.Web.Startup))]
namespace Jin.Tools.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
