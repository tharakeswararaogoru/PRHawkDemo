using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PRHawkDemo.Startup))]
namespace PRHawkDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
