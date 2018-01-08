using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CivicdAPI.Startup))]
namespace CivicdAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
