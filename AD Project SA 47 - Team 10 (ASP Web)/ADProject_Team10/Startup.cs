using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ADProject_Team10.Startup))]
namespace ADProject_Team10
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
