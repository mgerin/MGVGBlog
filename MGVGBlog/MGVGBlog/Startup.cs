using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MGVGBlog.Startup))]
namespace MGVGBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
