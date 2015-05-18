using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WaiwardDemo.Startup))]
namespace WaiwardDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
