using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DvdStore.Startup))]
namespace DvdStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
