using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cartelux1.Startup))]
namespace Cartelux1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
