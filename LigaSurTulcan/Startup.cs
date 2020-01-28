using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LigaSurTulcan.Startup))]
namespace LigaSurTulcan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
