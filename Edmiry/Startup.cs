using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Edmiry.Startup))]
namespace Edmiry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
