using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MCT.Web.Startup))]
namespace MCT.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
