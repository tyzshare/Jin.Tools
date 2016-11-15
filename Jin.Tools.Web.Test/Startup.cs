using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jin.Tools.Web.Test.Startup))]
namespace Jin.Tools.Web.Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
