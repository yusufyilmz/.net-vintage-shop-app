using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YSVintageShop.Startup))]
namespace YSVintageShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
